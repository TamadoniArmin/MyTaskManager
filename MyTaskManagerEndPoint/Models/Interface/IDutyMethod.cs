namespace MyTaskManagerEndPoint.Models.Interface
{
    public interface IDutyMethod
    {
        public bool CanWriteNewDuty(int UserId);
    }
}
