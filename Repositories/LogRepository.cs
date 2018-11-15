using Data;
using Services.IdentityServer;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Data.ViewModels;
using System.Threading.Tasks;

namespace Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly BefordingTestContext _context;
        private readonly IUserInfoService _userInfoService;

        public LogRepository(BefordingTestContext context, IUserInfoService userInfoService)
        {
            _context = context;
            _userInfoService = userInfoService;
        }

        public async Task AddLog(string userId, string message)
        {
            var newLog = new Log()
            {
                Severity = "Error",
                Message = message,
                UserId = userId,
                TimeOfOccurrence = DateTime.Now
            };

            _context.Logs.Add(newLog);
            await _context.SaveChangesAsync();
        }

        public async Task AddLogFE(LogVM logVM)
        {
            var userId = _userInfoService.UserId;

            var newLog = new Log()
            {
                Severity = logVM.Severity,
                Message = logVM.Message,
                UserId = userId,
                TimeOfOccurrence = DateTime.Now
            };

            _context.Logs.Add(newLog);
            await _context.SaveChangesAsync();
        }
    }
}
