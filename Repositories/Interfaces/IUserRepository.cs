using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.ViewModels;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(UserVM newUser);
        Task<User> ChangePassword(ChangePasswordVM changePasswordVm);
    }
}
