using Exam.App.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Infrastructure.Database;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var context = serviceProvider.GetRequiredService<AppDbContext>();

        // Seed Administrators
        await SeedUser(userManager, "john", "john.doe@example.com", "John", "Doe", "John123!", "Administrator");
        await SeedUser(userManager, "jane", "jane.doe@example.com", "Jane", "Doe", "Jane123!", "Administrator");

        // Seed Users
        await SeedUser(userManager, "alice", "alice.smith@example.com", "Alice", "Smith", "Alice123!", "User");
        await SeedUser(userManager, "bob", "bob.jones@example.com", "Bob", "Jones", "Bobj123!", "User");
        await SeedUser(userManager, "charlie", "charlie.brown@example.com", "Charlie", "Brown", "Charlie123!", "User");
        await SeedUser(userManager, "diana", "diana.prince@example.com", "Diana", "Prince", "Diana123!", "User");
        await SeedUser(userManager, "edward", "edward.norton@example.com", "Edward", "Norton", "Edward123!", "User");
        await SeedUser(userManager, "fiona", "fiona.apple@example.com", "Fiona", "Apple", "Fiona123!", "User");
        await SeedUser(userManager, "george", "george.lucas@example.com", "George", "Lucas", "George123!", "User");
        await SeedUser(userManager, "hannah", "hannah.montana@example.com", "Hannah", "Montana", "Hannah123!", "User");
        await SeedUser(userManager, "ivan", "ivan.petrov@example.com", "Ivan", "Petrov", "Ivanp123!", "User");
        await SeedUser(userManager, "julia", "julia.roberts@example.com", "Julia", "Roberts", "Julia123!", "User");
        await SeedUser(userManager, "kevin", "kevin.hart@example.com", "Kevin", "Hart", "Kevin123!", "User");
        await SeedUser(userManager, "laura", "laura.palmer@example.com", "Laura", "Palmer", "Laura123!", "User");
        await SeedUser(userManager, "mike", "mike.tyson@example.com", "Mike", "Tyson", "Miket123!", "User");
        await SeedUser(userManager, "nina", "nina.simone@example.com", "Nina", "Simone", "Ninas123!", "User");
        await SeedUser(userManager, "oscar", "oscar.wilde@example.com", "Oscar", "Wilde", "Oscar123!", "User");

        // Seed Projects
        await SeedProjects(context, userManager);
    }

    private static async Task SeedProjects(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        if (await context.Projects.AnyAsync()) return;

        var alice = await userManager.FindByNameAsync("alice");
        var bob = await userManager.FindByNameAsync("bob");
        var charlie = await userManager.FindByNameAsync("charlie");

        var projects = new List<Project>
        {
            // Alice: 1 draft, 1 published, 2 completed, 0 archived
            new Project { Name = "Alice Draft App", Description = "A draft mobile app concept", StartedAt = new DateTime(2025, 1, 10, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Draft, CompletedAt = null, UserId = alice!.Id },
            new Project { Name = "Alice Website Redesign", Description = "Redesigning the company website", StartedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Published, CompletedAt = null, UserId = alice.Id },
            new Project { Name = "Alice Inventory System", Description = "Warehouse inventory tracking system", StartedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc), CompletedAt = new DateTime(2024, 5, 20, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Completed, UserId = alice.Id },
            new Project { Name = "Alice Data Migration", Description = "Legacy database migration project", StartedAt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc), CompletedAt = new DateTime(2024, 7, 10, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Completed, UserId = alice.Id },

            // Bob: 0 draft, 2 published, 1 completed, 1 archived
            new Project { Name = "Bob API Platform", Description = "REST API platform for partners", StartedAt = new DateTime(2024, 8, 5, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Published, CompletedAt = null, UserId = bob!.Id },
            new Project { Name = "Bob Chat Service", Description = "Real-time messaging service", StartedAt = new DateTime(2024, 9, 12, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Published, CompletedAt = null, UserId = bob.Id },
            new Project { Name = "Bob Auth Module", Description = "Authentication and authorization module", StartedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc), CompletedAt = new DateTime(2024, 4, 15, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Completed, UserId = bob.Id },
            new Project { Name = "Bob Legacy Portal", Description = "Old customer portal now archived", StartedAt = new DateTime(2023, 5, 1, 0, 0, 0, DateTimeKind.Utc), CompletedAt = new DateTime(2023, 12, 1, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Archived, UserId = bob.Id },

            // Charlie: 1 draft, 1 published, 3 completed, 1 archived
            new Project { Name = "Charlie ML Pipeline", Description = "Machine learning data pipeline", StartedAt = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Draft, CompletedAt = null, UserId = charlie!.Id },
            new Project { Name = "Charlie Dashboard", Description = "Analytics dashboard for sales team", StartedAt = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Published, CompletedAt = null, UserId = charlie.Id },
            new Project { Name = "Charlie CRM Integration", Description = "CRM system integration with ERP", StartedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), CompletedAt = new DateTime(2024, 3, 30, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Completed, UserId = charlie.Id },
            new Project { Name = "Charlie Email Service", Description = "Transactional email microservice", StartedAt = new DateTime(2024, 4, 10, 0, 0, 0, DateTimeKind.Utc), CompletedAt = new DateTime(2024, 6, 25, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Completed, UserId = charlie.Id },
            new Project { Name = "Charlie Report Generator", Description = "Automated PDF report generation", StartedAt = new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc), CompletedAt = new DateTime(2024, 9, 15, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Completed, UserId = charlie.Id },
            new Project { Name = "Charlie Old Website", Description = "Previous company website now archived", StartedAt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc), CompletedAt = new DateTime(2023, 6, 1, 0, 0, 0, DateTimeKind.Utc), Status = ProjectStatus.Archived, UserId = charlie.Id },
        };

        context.Projects.AddRange(projects);
        await context.SaveChangesAsync();
    }

    private static async Task SeedUser(UserManager<ApplicationUser> userManager, string username, string email, string name, string surname, string password, string role)
    {
        if (await userManager.FindByNameAsync(username) == null)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                Name = name,
                Surname = surname,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
