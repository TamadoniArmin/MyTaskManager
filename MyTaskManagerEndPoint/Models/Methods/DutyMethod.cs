using App.Domain.Core.MyTaskManager.Duties.App.Domain.Core;
using MyTaskManagerEndPoint.Models.Interface;

namespace MyTaskManagerEndPoint.Models.Methods
{
    public class DutyMethod : IDutyMethod
    {
        private readonly IConfiguration _appsetting;
        private readonly IDutyAppService _service;
        public DutyMethod(IConfiguration appsetting, IDutyAppService dutyAppService)
        {
            _appsetting = appsetting;
            _service = dutyAppService;
        }
        public bool CanWriteNewDuty(int UserId)
        {
            int Limit = int.Parse(_appsetting["ظرفیت مجاز وظایف تکمیل نشده"]);
            int Count = _service.NumberOfNotCompleted(UserId);
            if (Count >= Limit)
            {
                return false;
            }
            return true;
        }
    }
}
