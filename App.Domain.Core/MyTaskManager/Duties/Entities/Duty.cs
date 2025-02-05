using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.MyTaskManager.Users.Entities;

namespace App.Domain.Core.MyTaskManager.Duties.Entities
{
    public class Duty
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }=false;
        //public bool OnceChanged { get; set; } = false;
        public int UserId { get; set; }
        public User BelongTo { get; set; }
    }
}
