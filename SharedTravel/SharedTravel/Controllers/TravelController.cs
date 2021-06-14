using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedTravel.Data;
using SharedTravel.Models;
using SharedTravel.Services;
using System.Collections.Generic;
using System.Linq;
namespace SharedTravel.Controllers
{
    public class TravelController : Controller
    {
        AppDbContext _context;
        TravelService travelService;
        UserService userService;
        UserTravelService utService;
        PendingInviteService piService;

        public string Username;

        public TravelController(AppDbContext context)
        {
            _context = context;
            travelService = new TravelService(_context);
            userService = new UserService(_context);
            utService = new UserTravelService(_context);
            piService = new PendingInviteService(_context);
        }

        // GET: TravelController
        public ActionResult Index()
        {
            Username = Request.Cookies["User"];

            TravelsList travelsList = new TravelsList();

            if (!string.IsNullOrEmpty(Username))
            {
                var travels = travelService.GetAll().ToList();
                var user = userService.GetByUsername(Username);

                travels = travels.Where(t => t.CreatorId == user.UserId).ToList();
                foreach (var t in travels)
                {
                    t.Creator = userService.GetById(t.CreatorId);
                }

                travelsList.Travels = travels;

                return View(travelsList);
            }
            return RedirectToAction("Login", "User");
        }


        public ActionResult OtherTravels()
        {
            Username = Request.Cookies["User"];
            TravelsList travelsList = new TravelsList();

            if (!string.IsNullOrEmpty(Username))
            {
                var travels = travelService.GetAll().ToList();
                var user = userService.GetByUsername(Username);
                var users = userService.GetAll();

                travels = travels.Where(t => t.CreatorId != user.UserId).ToList();
                foreach (var t in travels)
                {
                    t.Creator = userService.GetById(t.CreatorId);
                }

                travelsList.Travels = travels;

                return View("Index", travelsList);
            }
            return RedirectToAction("Login", "User");
        }


        // GET: TravelController/Create
        public ActionResult Create()
        {
            Username = Request.Cookies["User"];

            if (string.IsNullOrEmpty(Username))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }


        // POST: TravelController/Create
        [HttpPost]
        public ActionResult Create(Travel travelToCreate)
        {
            Username = Request.Cookies["User"];
            if (!string.IsNullOrEmpty(Username))
            {
                User loggedUser = userService.GetByUsername(Username);
                //travelToCreate.Creator = loggedUser;
                travelToCreate.CreatorId = loggedUser.UserId;

                travelService.Create(travelToCreate);
                return RedirectToAction("Index");
            }


            return View(travelToCreate);
        }


        // GET: TravelController/Delete/5
        public ActionResult Delete(int id)
        {
            travelService.Delete(id);
            utService.DeleteByTravelId(id);

            return RedirectToAction("Index", "Home");
        }


        public ActionResult DeleteUT(int id, int secondid)
        {
            utService.Delete(id, secondid);
            var travel = travelService.GetById(secondid);

            travel.FreeSeatsCount += 1;
            travelService.Update(travel);


            return RedirectToAction("Index");
        }


        // GET: TravelController/Details/5
        public ActionResult Details(int id)
        {
            var currentTravel = travelService.GetById(id);

            List<User> acceptedUsers = new List<User>();
            List<User> pendingUsers = new List<User>();

            var acceptedUsersTravels = utService.GetByTravelId(id).ToList();
            foreach (var item in acceptedUsersTravels)
            {
                acceptedUsers.Add(userService.GetById(item.UserId));
            }

            var pendingInvites = piService.GetByTravelId(id).ToList();
            foreach (var item in pendingInvites)
            {
                pendingUsers.Add(userService.GetById(item.UserId));
            }

            ViewData["acceptedUsers"] = acceptedUsers;
            ViewData["pendingUsers"] = pendingUsers;


            return View(currentTravel);
        }


        // GET: TravelController/Edit/5
        public ActionResult Edit(int id)
        {
            var travelToEdit = travelService.GetById(id);

            return View(travelToEdit);
        }


        // POST: TravelController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Travel newTravel)
        {
            var t = travelService.GetById(id);
            newTravel.TravelId = id;
            newTravel.CreatorId = t.CreatorId;

            var whatever = travelService.Update(newTravel);
            return RedirectToAction("Index");
        }


        public ActionResult Apply(int id)
        {
            Username = Request.Cookies["User"];
            var userId = userService.GetByUsername(Username).UserId;
            var pi = piService.GetById(userId, id);

            if (pi == null)
            {
                piService.Create(new PendingInvite
                {
                    UserId = userId,
                    TravelId = id
                });

            }
            return RedirectToAction("Index", "Home");

        }


        public ActionResult Accept(int id, int secondid)
        {
            Username = Request.Cookies["User"];

            var travel = travelService.GetById(secondid);

            if (travel.FreeSeatsCount > 0)
            {
                travel.FreeSeatsCount -= 1;
                travelService.Update(travel);

                piService.Delete(id, secondid);

                utService.Create(new UserTravel
                {
                    UserId = id,
                    TravelId = secondid
                });
            }
            else
            {
                ModelState.AddModelError("CountError", "No more free seats");
            }

            var travels = travelService.GetAll().ToList();
            var user = userService.GetByUsername(Username);

            TravelsList travelsList = new TravelsList();

            travels = travels.Where(t => t.CreatorId == user.UserId).ToList();
            foreach (var t in travels)
            {
                t.Creator = userService.GetById(t.CreatorId);
            }

            travelsList.Travels = travels;

            return View("~/Views/Travel/Index.cshtml", travelsList);
        }


        public ActionResult Reject(int id, int secondid)
        {
            Username = Request.Cookies["User"];

            piService.Delete(id, secondid);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Search(TravelsList travelsList)
        {
            Username = Request.Cookies["User"];

            if (!string.IsNullOrEmpty(Username))
            {
                var travels = travelService.GetAll().ToList();
                var user = userService.GetByUsername(Username);
                var users = userService.GetAll();

                travels = travels.Where(t => t.CreatorId != user.UserId).ToList();
                foreach (var t in travels)
                {
                    t.Creator = userService.GetById(t.CreatorId);
                }


                //if searched name is null, replace with ""
                //travelsList.Travels = travels.Where(x => x.Creator.Username.Contains(travelsList.SearchedName ?? ""));

                travelsList.Travels = travels.Where(x => x.Creator.FirstName.Contains(travelsList.SearchedName ?? ""));

                return View("Index", travelsList);
            }
            return RedirectToAction("Login", "User");
        }
    }
}
