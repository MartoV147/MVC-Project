using SharedTravel.Data;
using SharedTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTravel.Services
{
    public class UserTravelService
    {
        AppDbContext context;

        public UserTravelService(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<UserTravel> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var userTravels = unitOfWork.UserTravelRepo.GetAll();

                return userTravels.Select(userTravels => new UserTravel
                {
                    UserId = userTravels.UserId,
                    TravelId = userTravels.TravelId
                });
            }
        }


        public UserTravel GetById(int userId, int travelId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var userTravel = unitOfWork.UserTravelRepo.GetUTById(userId, travelId);

                return userTravel == null ? null : new UserTravel
                {
                    UserId = userTravel.UserId,
                    TravelId = userTravel.TravelId
                };
            }
        }

        public IEnumerable<UserTravel> GetByUserId(int userId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.UserTravelRepo.GetAll().Where(u => u.UserId == userId);

                return result;
            }
        }


        public IEnumerable<UserTravel> GetByTravelId(int travelId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.UserTravelRepo.GetAll().Where(u => u.TravelId == travelId);

                return result;
            }
        }


        public bool Create(UserTravel userTravel)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var newUserTravel = new UserTravel()
                {
                    UserId = userTravel.UserId,
                    TravelId = userTravel.TravelId
                };

                unitOfWork.UserTravelRepo.Create(newUserTravel);
                return unitOfWork.Save();
            }
        }


        public bool Update(UserTravel userTravel)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.UserTravelRepo.GetAll().Where(x => x.UserId == userTravel.UserId
                && x.TravelId == userTravel.TravelId).FirstOrDefault();

                if (result == null)
                {
                    return false;
                }

                result.UserId = userTravel.UserId;
                result.TravelId = userTravel.TravelId;

                unitOfWork.UserTravelRepo.Update(result);
                return unitOfWork.Save();
            }
        }


        public bool Delete(int userId, int travelId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                UserTravel result = unitOfWork.UserTravelRepo.GetUTById(userId, travelId);

                if (result == null)
                {
                    return false;
                }

                unitOfWork.UserTravelRepo.Delete(result);
                return unitOfWork.Save();
            }
        }

        public bool DeleteByTravelId(int travelId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.UserTravelRepo.GetAll().Where(u => u.TravelId == travelId);

                if (result == null)
                {
                    return false;
                }

                foreach (var item in result)
                {
                    unitOfWork.UserTravelRepo.Delete(item);
                }
                return unitOfWork.Save();
            }
        }

        public bool DeleteByUserId(int userId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.UserTravelRepo.GetAll().Where(u => u.UserId == userId);

                if (result == null)
                {
                    return false;
                }

                foreach (var item in result)
                {
                    unitOfWork.UserTravelRepo.Delete(item);
                }
                return unitOfWork.Save();
            }
        }
    }
}
