using Exam.App.Domain;
using Microsoft.AspNetCore.Identity;

namespace Exam.App.Infrastructure.Database;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var admin1 = new ApplicationUser
        {
            UserName = "john",
            Email = "john.doe@example.com",
            Name = "John",
            Surname = "Doe",
            EmailConfirmed = true
        };

        if (await userManager.FindByNameAsync(admin1.UserName) == null)
        {
            await userManager.CreateAsync(admin1, "John123!");
            await userManager.AddToRoleAsync(admin1, "Administrator");
        }

        var admin2 = new ApplicationUser
        {
            UserName = "jane",
            Email = "jane.doe@example.com",
            Name = "Jane",
            Surname = "Doe",
            EmailConfirmed = true
        };

        if (await userManager.FindByNameAsync(admin2.UserName) == null)
        {
            await userManager.CreateAsync(admin2, "Jane123!");
            await userManager.AddToRoleAsync(admin2, "Administrator");
        }
    }
}