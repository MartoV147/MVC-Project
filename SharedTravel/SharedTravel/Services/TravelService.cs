using SharedTravel.Data;
using SharedTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTravel.Services
{
    public class TravelService
    {
        AppDbContext context;


        public TravelService(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Travel> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var travels = unitOfWork.TravelRepo.GetAll();

                return travels;
            }
        }

        public Travel GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var travel = unitOfWork.TravelRepo.GetById(id);

                return travel == null ? null : new Travel
                {
                    TravelId = travel.TravelId,
                    Creator = travel.Creator,
                    CreatorId = travel.CreatorId,
                    CityFrom = travel.CityFrom,
                    AddressFrom = travel.AddressFrom,
                    DepartureTime = travel.DepartureTime,
                    CityTo = travel.CityTo,
                    AddressTo = travel.AddressTo,
                    ArrivalTime = travel.ArrivalTime,
                    FreeSeatsCount = travel.FreeSeatsCount,
                    Price = travel.Price
                };
            }
        }

        public IEnumerable<Travel> GetByCreator(User user)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var travels = unitOfWork.TravelRepo.GetAll(u => u.Creator == user).ToList();

                return travels;
            }
        }

        public IEnumerable<Travel> GetByCityFrom(string cityFrom)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var travels = unitOfWork.TravelRepo.GetAll(u => u.CityFrom == cityFrom).ToList();

                return travels;
            }
        }

        public IEnumerable<Travel> GetByCityTo(string cityTo)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var travels = unitOfWork.TravelRepo.GetAll(u => u.CityTo == cityTo).ToList();

                return travels;
            }
        }

        public IEnumerable<Travel> GetByDepartureTime(DateTime departureTime)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var travels = unitOfWork.TravelRepo.GetAll(u => u.DepartureTime.CompareTo(departureTime) == 0).ToList();

                return travels;
            }
        }

        public IEnumerable<Travel> GetByArrivalTime(DateTime arrivalTime)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var travels = unitOfWork.TravelRepo.GetAll(u => u.ArrivalTime.CompareTo(arrivalTime) == 0).ToList();

                return travels;
            }
        }




        public bool Create(Travel travel)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var newTravel = new Travel()
                {
                    TravelId = travel.TravelId,
                    Creator = travel.Creator,
                    CreatorId = travel.CreatorId,
                    CityFrom = travel.CityFrom,
                    AddressFrom = travel.AddressFrom,
                    DepartureTime = travel.DepartureTime,
                    CityTo = travel.CityTo,
                    AddressTo = travel.AddressTo,
                    ArrivalTime = travel.ArrivalTime,
                    FreeSeatsCount = travel.FreeSeatsCount,
                    Price = travel.Price
                };
                unitOfWork.TravelRepo.Create(newTravel);

                return unitOfWork.Save();
            }
        }

        public bool Update(Travel travel)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.TravelRepo.GetById(travel.TravelId);

                if (result == null)
                {
                    return false;
                }

                result.TravelId = travel.TravelId;
                result.Creator = travel.Creator;
                result.CreatorId = travel.CreatorId;
                result.CityFrom = travel.CityFrom;
                result.AddressFrom = travel.AddressFrom;
                result.DepartureTime = travel.DepartureTime;
                result.CityTo = travel.CityTo;
                result.AddressTo = travel.AddressTo;
                result.ArrivalTime = travel.ArrivalTime;
                result.FreeSeatsCount = travel.FreeSeatsCount;
                result.Price = travel.Price;

                unitOfWork.TravelRepo.Update(result);
                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                Travel result = unitOfWork.TravelRepo.GetById(id);

                if (result == null)
                {
                    return false;
                }

                unitOfWork.TravelRepo.Delete(result);
                return unitOfWork.Save();
            }
        }
    }
}