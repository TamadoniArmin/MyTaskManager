using App.Domain.Core.MyTaskManager.Duties.Entities;
using App.Domain.Core.MyTaskManager.Users.Entities;
using Connection.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Connection.DbContext
{
    public class MyTaskManagerDbContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
        public MyTaskManagerDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DutyConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Duty> Duties { get; set; }
    }
}
