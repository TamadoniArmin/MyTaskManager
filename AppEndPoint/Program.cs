using App.Domain.Core.MyTaskManager.Duties.App.Domain.Core;
using App.Domain.Core.MyTaskManager.Duties.Data.Repositories;
using App.Domain.Core.MyTaskManager.Duties.Service;
using App.Domain.Core.MyTaskManager.Users.App.Domain.Core;
using App.Domain.Core.MyTaskManager.Users.Entities;
using App.Infra.Data.Repos.Ef.MyTaskManager.Duties;
using AppEndPoint.Models.Interface;
using Connection.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyTaskManagerAppService.MyTaskManager.Duties;
using MyTaskManagerAppService.MyTaskManager.Users;
using MyTaskManagerEndPoint.Models.Methods;
using MyTaskManagerService.Duties;

namespace AppEndPoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<MyTaskManagerDbContext>(options => options.UseSqlServer("Data Source=DESKTOP-7D9S1GO;Initial Catalog=MyTaskManager;Integrated Security=SSPI;TrustServerCertificate=True;"));
            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).
            AddRoles<IdentityRole<int>>().AddEntityFrameworkStores<MyTaskManagerDbContext>();





            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true);
            //.AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDutyRepository, DutyRepository>();
            builder.Services.AddScoped<IDutyService, DutyService>();
            builder.Services.AddScoped<IDutyAppService, DutyAppService>();
            builder.Services.AddScoped<IUserAppService, UserAppService>();
            builder.Services.AddScoped<IDutyMethod, DutyMethod>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
