using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.MyTaskManager.Duties.Entities;
using App.Domain.Core.MyTaskManager.Users.Entities;

namespace App.Domain.Core.MyTaskManager.Duties.App.Domain.Core
{
    public interface IDutyAppService
    {
        public Task<List<Duty>> GetAllDuties(int UserId);
        public Task<List<Duty>> GetListOfCompletedDuties(int UserId);
        public Task<List<Duty>> GetListOfNotComletedDuties(int UserId);
        public Duty CheckTitle(int UserId, string Title);
        public bool AddDuty(string Title, string Description, User user);
        public bool UpdateDuty(Duty duty, int DutyId);
        public bool DeleteDuty(int id);
        public Duty GetDutyById(int id);
        public Task<List<Duty>> GetDutyByTitle(int UserId, string Title);
        public bool MarkAsCompleted(int id);
        public int NumberOfNotCompleted(int Userid);
    }
}
