using Data;
using Data.ViewModels;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BefordingTestContext _context;

        public UserRepository(BefordingTestContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(UserVM newUser)
        {

            bool IsAlreadyRegistred = _context.Users.Select(x => x.Email == newUser.Email).FirstOrDefault();

            if (IsAlreadyRegistred)
            {
                return null;
            }
            else
            {
                var dummyUser = new User()
                {
                    Email = newUser.Email,
                    Password = newUser.Password,
                    Role = "User"
                };
                _context.Users.Add(dummyUser);
                await _context.SaveChangesAsync();

                return dummyUser;
            }
        }

        public async Task<User> ChangePassword(ChangePasswordVM changePasswordVm)
        {
            var user = _context.Users.FirstOrDefault(x => changePasswordVm.UserId == x.Id);
            if (user == null)
            {
                return null;
            }

            if (user.Password != changePasswordVm.OldPassword)
            {
                return null;
            }

            user.Password = changePasswordVm.NewPassword;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
