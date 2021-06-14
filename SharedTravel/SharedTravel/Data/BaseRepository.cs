using Microsoft.EntityFrameworkCore;
using SharedTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SharedTravel.Data
{
    public class BaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly AppDbContext dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            Entities = dbContext.Set<TEntity>();
        }

        protected DbSet<TEntity> Entities { get; private set; }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return Entities
                    .Where(filter)
                    .ToList();
            }
            return dbContext.Set<TEntity>().ToList();
        }

        public virtual TEntity GetById(int id)
        {
            return Entities.Find(id);
        }


        public UserTravel GetUTById(int userId, int travelId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                var result = unitOfWork.UserTravelRepo.GetAll().Where(x => x.UserId == userId && x.TravelId == travelId).FirstOrDefault();
                return result;
            }
        }

        public PendingInvite GetPIById(int userId, int travelId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                var result = unitOfWork.PendingInviteRepo.GetAll().Where(x => x.UserId == userId && x.TravelId == travelId).FirstOrDefault();
                return result;
            }
        }


        public virtual TEntity Create(TEntity entity)
        {
            Entities.Add(entity);
            return entity;
        }


        public virtual void Update(TEntity entity)
        {
            Entities.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            Entities.Remove(entity);
        }
    }
}