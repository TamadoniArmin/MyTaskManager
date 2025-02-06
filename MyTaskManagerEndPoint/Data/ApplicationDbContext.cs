namespace MyTaskManagerEndPoint.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    namespace MyTaskManagerEndPoint.Data
    {
        public class ApplicationDbContext : IdentityDbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }
        }
    }
}
