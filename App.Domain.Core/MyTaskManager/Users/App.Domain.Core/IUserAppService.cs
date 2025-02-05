using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.MyTaskManager.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.MyTaskManager.Users.App.Domain.Core
{
    public interface IUserAppService
    {
        public Task<IdentityResult> Register(User user,string Password,CancellationToken cancellationToken);
        public Task<IdentityResult> Login(string UserName,string Password);
    }
}
