using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.MyTaskManager.Duties.App.Domain.Core;
using App.Domain.Core.MyTaskManager.Duties.Entities;
using App.Domain.Core.MyTaskManager.Duties.Service;
using App.Domain.Core.MyTaskManager.Users.Entities;

namespace MyTaskManagerAppService.MyTaskManager.Duties
{
    public class DutyAppService : IDutyAppService
    {
        private readonly IDutyService _service;
        public DutyAppService(IDutyService service)
        {
            _service = service;
        }

        public bool AddDuty(string Title, string Description,User user)
        {
            var FindDuty=CheckTitle(user.Id,Title);
            if (FindDuty is not null)
            {
                return false;
            }
            else
            {
                Duty duty = new Duty();
                duty.Title = Title;
                duty.Description = Description;
                duty.UserId = user.Id;
                duty.BelongTo = user;
                //duty.Id = 2;
                return _service.AddDuty(duty);
            }
        }

        public Duty CheckTitle(int UserId, string Title)
        {
            return _service.CheckTitle(UserId, Title);
        }

        public bool DeleteDuty(int id)
        {
            return (_service.DeleteDuty(id));
        }

        public async Task<List<Duty>> GetAllDuties(int UserId)
        {
            return await _service.GetAllDuties(UserId);
        }

        public Duty GetDutyById(int id)
        {
            return _service.GetDutyById(id);
        }

        public async Task<List<Duty>> GetDutyByTitle(int UserId, string Title)
        {
            return await _service.GetDutyByTitle(UserId,Title);
        }

        public async Task<List<Duty>> GetListOfCompletedDuties(int UserId)
        {
            return await _service.GetListOfCompletedDuties(UserId);
        }

        public async Task<List<Duty>> GetListOfNotComletedDuties(int UserId)
        {
            return await _service.GetListOfNotComletedDuties(UserId);
        }

        public bool MarkAsCompleted(int id)
        {
            return _service.MarkAsCompleted(id);
        }

        public int NumberOfNotCompleted(int Userid)
        {
            return _service.NumberOfNotCompleted(Userid);
        }

        public bool UpdateDuty(Duty duty,int DutyId)
        {
            return _service.UpdateDuty(duty, DutyId);
        }
    }
}
