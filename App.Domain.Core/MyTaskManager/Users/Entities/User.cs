
using App.Domain.Core.MyTaskManager.Duties.Entities;
using Microsoft.AspNetCore.Identity;


namespace App.Domain.Core.MyTaskManager.Users.Entities
{
    public class User : IdentityUser<int>
    {
        public string Email { get; set; }
        public List<Duty>? UserDuties { get; set; }
        public List<Duty>? UnDoneDuties { get; set; }
    }
}
