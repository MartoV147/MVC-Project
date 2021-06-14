using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedTravel.Data;
using SharedTravel.Models;
using SharedTravel.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTravel.Controllers
{
    public class UserController : Controller
    {
        AppDbContext _context;
        UserService userService;

        public UserController(AppDbContext context)
        {
            _context = context;
            userService = new UserService(_context);
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View(userService.GetAll().ToList());
        }


        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Response.Cookies.Append("User", string.Empty);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult Login(User userToLogin)
        {
            User u = userService.GetByUsername(userToLogin.Username);
            if (u == null)
            {
                ModelState.AddModelError("LoginError", "No such user found");
            }

            if (u != null && userToLogin.Password == u.Password)
            {
                Response.Cookies.Append("User", u.Username);

                return RedirectToAction("Index", "Home");
            }
            return View(userToLogin);
        }



        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        public ActionResult Create(User userToCreate)
        {
            if (!ModelState.IsValid)
                return View(userToCreate);

            if (userService.ValidateUserRegistation(userToCreate.Username))
            {
                userService.Create(userToCreate);

                return RedirectToAction("Login");
            }
            
            ModelState.AddModelError("RegisterError", "That Username already exists"); 
            return View(userToCreate);
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User editedUserModel)
        {
            if (!ModelState.IsValid)
                return View(editedUserModel);

            if (userService.GetById(id) == null)
            {
                ModelState.AddModelError("EditError", "Could not find user with ID: " + id);
                return View(editedUserModel);
            }
            editedUserModel.UserId = id;
            userService.Update(editedUserModel);

            return RedirectToAction("Index");
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
