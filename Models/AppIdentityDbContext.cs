using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RottenPotatoes.Models
{
  public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
  {
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }
  }
  public static class IdentitySeedData
  {
    private const string adminUser = "Admin";
    private const string adminPassword = "Secret123$";
    private static string[][] default_users = new string[][] {
      new string[]{"user1", "Haslo12#"}, 
      new string[]{"user2", "Haslo12#"}, 
      new string[]{"user3", "Haslo12#"}, 
      new string[]{"user4", "Haslo12#"},
      new string[]{"user5", "Haslo12#"}
    };

    public static async void EnsurePopulated(IApplicationBuilder app)
    {
      using (var scope = app.ApplicationServices.CreateScope())
      {
        var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
        IdentityUser admin = await userManager.FindByIdAsync(adminUser);
        if (admin == null)
        {
          admin = new IdentityUser(adminUser);
          await userManager.CreateAsync(admin, adminPassword);
        }
        foreach (var u in default_users)
        {
          IdentityUser user = await userManager.FindByIdAsync(u[0]);
          if (user == null)
          {
            user = new IdentityUser(u[0]);
            await userManager.CreateAsync(user, u[1]);
          }
        }
      }
    }
  }
}