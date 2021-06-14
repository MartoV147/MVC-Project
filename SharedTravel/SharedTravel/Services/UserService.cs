using SharedTravel.Data;
using SharedTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTravel.Services
{
    public class UserService
    {
        AppDbContext context;


        public UserService(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var users = unitOfWork.UserRepo.GetAll();

                return users;
            }
        }

        public User GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var user = unitOfWork.UserRepo.GetById(id);

                return user == null ? null : new User
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }
        }

        public User GetByUsername(string username)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var users = unitOfWork.UserRepo.GetAll(u => u.Username == username).ToList();

                User result = users.FirstOrDefault();

                return result == null ? null : new User
                {
                    UserId = result.UserId,
                    Username = result.Username,
                    Password = result.Password,
                    FirstName = result.FirstName,
                    LastName = result.LastName
                };
            }
        }

        public bool Create(User user)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var newUser = new User()
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                
                unitOfWork.UserRepo.Create(newUser);
                
                return unitOfWork.Save();
            }
        }

        public bool Update(User user)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var result = unitOfWork.UserRepo.GetById(user.UserId);

                if (result == null)
                {
                    return false;
                }

                result.Username = user.Username;
                result.Password = user.Password;
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;

                unitOfWork.UserRepo.Update(result);
                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                User result = unitOfWork.UserRepo.GetById(id);

                if (result == null)
                {
                    return false;
                }

                unitOfWork.UserRepo.Delete(result);
                return unitOfWork.Save();
            }
        }


        public bool ValidateUserRegistation(string username) 
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var users = unitOfWork.UserRepo.GetAll(u => u.Username == username).ToList();

                /*if (users.Count == 0)
                    return true;
                else
                    return false;*/

                return users.Count() == 0;
            }
        }
    }
}