using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.ViewModels;

namespace Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Login(UserVM userVM);
    }
}
