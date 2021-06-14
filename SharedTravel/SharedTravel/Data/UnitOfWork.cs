using SharedTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTravel.Data
{
    public class UnitOfWork : IDisposable
    {
        private AppDbContext dbContext;
        private BaseRepository<User> userRepo;
        private BaseRepository<Travel> travelRepo;
       
        private BaseRepository<UserTravel> userTravelRepo;
        private BaseRepository<PendingInvite> pendingInviteRepo;



        private bool disposed = false;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public BaseRepository<User> UserRepo
        {
            get
            {
                if (userRepo == null)
                {
                    userRepo = new BaseRepository<User>(dbContext);
                }
                return userRepo;
            }
        }

        public BaseRepository<Travel> TravelRepo
        {
            get
            {
                if (travelRepo == null)
                {
                    travelRepo = new BaseRepository<Travel>(dbContext);
                }
                return travelRepo;
            }
        }

        public BaseRepository<UserTravel> UserTravelRepo
        {
            get
            {
                if (userTravelRepo == null)
                {
                    userTravelRepo = new BaseRepository<UserTravel>(dbContext);
                }
                return userTravelRepo;
            }
        }

        public BaseRepository<PendingInvite> PendingInviteRepo
        {
            get
            {
                if (pendingInviteRepo == null)
                {
                    pendingInviteRepo = new BaseRepository<PendingInvite>(dbContext);
                }
                return pendingInviteRepo;
            }
        }

        public bool Save()
        {
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public void Dispose()
        {}
    }
}
