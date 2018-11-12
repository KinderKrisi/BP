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
        Task AddLog(Log log);
        Task AddLogFE(LogVM log);
    }
}
