using System.Security.AccessControl;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WoundClinic.Models;
using WoundClinic.Services;
using WoundClinic.Services.IRepository;

namespace WoundClinic.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            

            // اعمال مایگریشن‌ها
            await context.Database.MigrateAsync();

            // ایجاد نقش پیش‌فرض
            var roleName = "مدیر سیستم";
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new ApplicationRole(roleName));
            }
            // افزودن person مرتبط با کاربر
            if (!context.Persons.Any())
            {
                var person = new Person
                {
                    FirstName="داود",
                    LastName="اقاویل جهرمی",
                    Gender= true,
                    NationalCode = 1285046358,
                };
                context.Persons.Add(person);
                await context.SaveChangesAsync();
            }

            // ایجاد کاربر پیش‌فرض
            var userName = "1285046358";
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "1285046358",
                    PersonNationalCode = 1285046358,
                    PhoneNumberConfirmed=true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin123!"); // رمز عبور قوی الزامی است
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }

        }
    }
}
