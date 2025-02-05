using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.MyTaskManager.Duties.Data.Repositories;
using App.Domain.Core.MyTaskManager.Duties.Entities;
using Connection.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.MyTaskManager.Duties
{
    public class DutyRepository : IDutyRepository
    {
        private readonly MyTaskManagerDbContext _dbContext;
        public DutyRepository(MyTaskManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool AddDuty(Duty duty)
        {
            var Duty=_dbContext.Duties.FirstOrDefault(x => x.Title== duty.Title || x.Description==duty.Description);
            if (Duty is null)
            {
                _dbContext.Duties.Add(duty);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public Duty CheckTitle(int UserId, string Title)
        {
            return _dbContext.Duties.FirstOrDefault(x => x.UserId == UserId && x.Title == Title);
        }

        public bool DeleteDuty(int id)
        {
            var Duty= _dbContext.Duties.FirstOrDefault(x=>x.Id==id);
            if(Duty is not null)
            {
                _dbContext.Duties.Remove(Duty);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<List<Duty>> GetAllDuties(int UserId)
        {
            return await _dbContext.Duties.Where(x=>x.UserId==UserId).ToListAsync();
        }

        public Duty GetDutyById(int id)
        {
            return _dbContext.Duties.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<Duty>> GetDutyByTitle(int UserId,string Title)
        {
            return await _dbContext.Duties.Where(x => x.UserId == UserId && x.Title.Contains(Title)).ToListAsync();
        }

        public async Task<List<Duty>> GetListOfCompletedDuties(int UserId)
        {
            return await _dbContext.Duties.Where(x => x.UserId == UserId && x.IsCompleted == true).ToListAsync();
        }

        public async Task<List<Duty>> GetListOfNotComletedDuties(int UserId)
        {
            return await _dbContext.Duties.Where(x => x.UserId == UserId && x.IsCompleted == false).ToListAsync();
        }

        public bool MarkAsCompleted(int id)
        {
            var Duty=_dbContext.Duties.FirstOrDefault(y=>y.Id==id);
            if(Duty is null || Duty.IsCompleted==true)
            {
                return false;
            }
            else
            {
                Duty.IsCompleted = true;
                _dbContext.SaveChanges();
                return true;
            }
        }

        public int NumberOfNotCompleted(int Userid)
        {
            return _dbContext.Duties.Count(x=>x.UserId==Userid && x.IsCompleted==false);
        }

        public bool UpdateDuty(Duty duty, int DutyId)
        {
            try
            {
                var Duty = _dbContext.Duties.FirstOrDefault(x => x.Id == DutyId);
                if(Duty is not null)
                {
                    Duty.Title=duty.Title;
                    Duty.Description=duty.Description;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
