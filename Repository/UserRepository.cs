using Carwash.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CarWashDbContext carwashdb;
        public UserRepository(CarWashDbContext carwashDb)
        {
            this.carwashdb = carwashDb;
        }
        public async Task<UserModel> AddAsync(UserModel user)
        {
            await carwashdb.AddAsync(user);
            await carwashdb.SaveChangesAsync();
            return user;
        }
        public async Task<UserModel> DeleteAsync(int id)
        {
            var user = await carwashdb.Usertable.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
            {
                return null;
            }
            carwashdb.Usertable.Remove(user);
            await carwashdb.SaveChangesAsync();
            return user;
        }
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await carwashdb.Usertable.ToListAsync();
            return users;
        }
        public async Task<UserModel> GetAsync(int id)
        {
            return await carwashdb.Usertable.FirstOrDefaultAsync(x => x.UserId == id);
        }
        public async Task<UserModel> UpdateAsync(int id, UserModel user)
        {
            var update = await carwashdb.Usertable.FirstOrDefaultAsync(x => x.UserId == id);
            if (update == null)
            {
                return null;
            }
            update.FirstName = user.FirstName;
            update.LastName = user.LastName;
            update.Email = user.Email;
            update.PhoneNo = user.PhoneNo;
            update.Password = user.Password;
            update.ConfirmPassword = user.ConfirmPassword;
            update.Address = user.Address;
            update.Role = user.Role;
            update.Status = user.Status;
            await carwashdb.SaveChangesAsync();
            return update;
        }
        public async Task<UserModel> Login(Login login)
        {
          
          
                var users = await carwashdb.Usertable.FirstOrDefaultAsync(x => x.Email == login.Email && x.Password == login.Password);
                if (users == null)
                    return null;

                return users;
            
           
        }
    }
}
   
