using SharedTravel.Data;
using SharedTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTravel.Services
{
    public class PendingInviteService
    {
        AppDbContext context;

        public PendingInviteService(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<PendingInvite> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var pendingInvites = unitOfWork.PendingInviteRepo.GetAll();

                return pendingInvites.Select(pendingInvites => new PendingInvite
                {
                    UserId = pendingInvites.UserId,
                    TravelId = pendingInvites.TravelId
                });
            }
        }


        public PendingInvite GetById(int userId, int travelId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var pendingInvite = unitOfWork.PendingInviteRepo.GetUTById(userId, travelId);

                return pendingInvite == null ? null : new PendingInvite
                {
                    UserId = pendingInvite.UserId,
                    TravelId = pendingInvite.TravelId
                };
            }
        }

        public IEnumerable<PendingInvite> GetByUserId(int userId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.PendingInviteRepo.GetAll().Where(u => u.UserId == userId);

                return result;
            }
        }


        public IEnumerable<PendingInvite> GetByTravelId(int travelId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.PendingInviteRepo.GetAll().Where(u => u.TravelId == travelId);

                return result;
            }
        }


        public bool Create(PendingInvite pendingInvite)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var newPendingInvite = new PendingInvite()
                {
                    UserId = pendingInvite.UserId,
                    TravelId = pendingInvite.TravelId
                };

                unitOfWork.PendingInviteRepo.Create(newPendingInvite);
                return unitOfWork.Save();
            }
        }


        public bool Update(PendingInvite pendingInvite)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.PendingInviteRepo.GetAll().Where(x => x.UserId == pendingInvite.UserId
                && x.TravelId == pendingInvite.TravelId).FirstOrDefault();

                if (result == null)
                {
                    return false;
                }

                result.UserId = pendingInvite.UserId;
                result.TravelId = pendingInvite.TravelId;

                unitOfWork.PendingInviteRepo.Update(result);
                return unitOfWork.Save();
            }
        }


        public bool Delete(int userId, int travelId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                PendingInvite result = unitOfWork.PendingInviteRepo.GetPIById(userId, travelId);

                if (result == null)
                {
                    return false;
                }

                unitOfWork.PendingInviteRepo.Delete(result);
                return unitOfWork.Save();
            }
        }

        public bool DeleteByTravelId(int travelId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.PendingInviteRepo.GetAll().Where(u => u.TravelId == travelId);

                if (result == null)
                {
                    return false;
                }

                foreach (var item in result)
                {
                    unitOfWork.PendingInviteRepo.Delete(item);
                }
                return unitOfWork.Save();
            }
        }

        public bool DeleteByUserId(int userId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.PendingInviteRepo.GetAll().Where(u => u.UserId == userId);

                if (result == null)
                {
                    return false;
                }

                foreach (var item in result)
                {
                    unitOfWork.PendingInviteRepo.Delete(item);
                }
                return unitOfWork.Save();
            }
        }
    }
}
