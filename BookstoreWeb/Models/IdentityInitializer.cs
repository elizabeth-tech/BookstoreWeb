using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWeb.Models
{
    public static class IdentityInitializer
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        //public static async void Initialize(IApplicationBuilder app)
        //{
        //    AppIdentityDbContext context = app.ApplicationServices
        //        .CreateScope().ServiceProvider
        //        .GetRequiredService<AppIdentityDbContext>();

        //    if (context.Database.GetPendingMigrations().Any())
        //        context.Database.Migrate();

        //    UserManager<IdentityUser> userManager = app.ApplicationServices
        //        .CreateScope().ServiceProvider
        //        .GetRequiredService<UserManager<IdentityUser>>();

        //    IdentityUser user = await userManager.FindByIdAsync(adminUser);

        //    if (user == null)
        //    {
        //        user = new IdentityUser("Admin");
        //        await userManager.CreateAsync(user, adminPassword);
        //    }
        //}

        public static async Task Initialize(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
