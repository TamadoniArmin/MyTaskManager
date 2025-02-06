
using App.Domain.Core.MyTaskManager.Duties.Entities;
using Microsoft.AspNetCore.Identity;


namespace App.Domain.Core.MyTaskManager.Users.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public List<Duty>? UserDuties { get; set; }
    }
}
