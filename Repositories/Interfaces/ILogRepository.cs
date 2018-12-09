using Data;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task AddLog(string userId, string message);
        Task AddLog(string userId, string message, int? id, bool isPatient);
        Task AddLogFE(LogVM log);
    }
}
