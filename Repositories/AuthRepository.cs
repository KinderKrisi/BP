using Data;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.ViewModels;

namespace Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly BefordingTestContext _context;

        public AuthRepository(BefordingTestContext context)
        {
            _context = context;
        }

        public async Task<User> Login(UserVM userVM)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == userVM.Email);
            if (user == null)
            {
                return null;
            }

            return user.Password == userVM.Password ? user : null;
        }
    }
}