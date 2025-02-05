using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.MyTaskManager.Duties.Data.Repositories;
using App.Domain.Core.MyTaskManager.Duties.Entities;
using App.Domain.Core.MyTaskManager.Duties.Service;

namespace MyTaskManagerService.Duties
{
    public class DutyService : IDutyService
    {
        private readonly IDutyRepository _repository;
        public DutyService(IDutyRepository repository)
        {
            _repository = repository;
        }
        public bool AddDuty(Duty duty)
        {
            return _repository.AddDuty(duty);
        }

        public Duty CheckTitle(int UserId, string Title)
        {
            return _repository.CheckTitle(UserId, Title);
        }

        public bool DeleteDuty(int id)
        {
            return (_repository.DeleteDuty(id));
        }

        public async Task<List<Duty>> GetAllDuties(int UserId)
        {
            return await _repository.GetAllDuties(UserId);
        }

        public Duty GetDutyById(int id)
        {
            return _repository.GetDutyById(id);
        }

        public async Task<List<Duty>> GetDutyByTitle(int UserId, string Title)
        {
            return await _repository.GetDutyByTitle(UserId, Title);
        }

        public async Task<List<Duty>> GetListOfCompletedDuties(int UserId)
        {
            return await _repository.GetListOfCompletedDuties(UserId);
        }

        public async Task<List<Duty>> GetListOfNotComletedDuties(int UserId)
        {
            return await _repository.GetListOfNotComletedDuties(UserId);
        }

        public bool MarkAsCompleted(int id)
        {
            return _repository.MarkAsCompleted(id);
        }

        public int NumberOfNotCompleted(int Userid)
        {
            return _repository.NumberOfNotCompleted(Userid);
        }

        public bool UpdateDuty(Duty duty, int DutyId)
        {
            return _repository.UpdateDuty(duty,DutyId);
        }
    }
}
