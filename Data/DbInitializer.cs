using Microsoft.AspNetCore.Identity;
using ComIT_eLearning.Models;

public static class DbInitializer
{
  public static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
  {
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var roles = new[] { "Admin", "Teacher", "Student" };

    foreach (var role in roles)
    {
      if (!await roleManager.RoleExistsAsync(role))
      {
        await roleManager.CreateAsync(new IdentityRole(role));
      }
    }

    string adminEmail = "ikulan12.admin@gmail.com";
    string adminPassword = "P@ssw0rd123";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
      var user = new ApplicationUser
      {
        FirstName = "Patty",
        LastName = "Liu",
        UserName = adminEmail,
        Email = adminEmail,
        EmailConfirmed = true
      };

      var result = await userManager.CreateAsync(user, adminPassword);
      if (result.Succeeded)
      {
        await userManager.AddToRoleAsync(user, "Admin");
      }
    }
  }
}
