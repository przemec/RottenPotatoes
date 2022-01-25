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
    public static async void EnsurePopulated(IApplicationBuilder app)
    {
      using (var scope = app.ApplicationServices.CreateScope())
      {
        var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
        IdentityUser user = await userManager.FindByIdAsync(adminUser);
        if (user == null)
        {
          user = new IdentityUser(adminUser);
          await userManager.CreateAsync(user, adminPassword);
        }
      }
    }
  }
}