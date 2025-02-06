using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.MyTaskManager.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connection.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasMany(x=>x.UserDuties).WithOne(x=>x.BelongTo).HasForeignKey(x=>x.Id).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
