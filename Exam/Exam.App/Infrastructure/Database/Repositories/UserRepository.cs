using Exam.App.Domain;
using Exam.App.Domain.Repositories;
using Exam.App.Services.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Infrastructure.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<PaginatedList<ApplicationUser>> GetAllAsync(int page, int pageSize)
    {
        var admins = await _userManager.GetUsersInRoleAsync("Administrator");
        var adminsId = admins.Select(a => a.Id).ToList();
        var userQuery = _context.Users.Where(u => !adminsId.Contains(u.Id));
        var totalCount = await userQuery.CountAsync();
        var items = await userQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<ApplicationUser>(items, page, pageSize, totalCount);
    }

    public async Task<ApplicationUser> Get(string id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<PaginatedList<UserProjectSummaryDto>> GetUsersWithProjectStatsAsync(int page, int pageSize)
    {
        var admins = await _userManager.GetUsersInRoleAsync("Administrator");
        var adminIds = admins.Select(a => a.Id).ToList();

        var userQuery = _context.Users
            .Where(u => !adminIds.Contains(u.Id));

        var totalCount = await userQuery.CountAsync();

        var items = await userQuery
            .Select(u => new UserProjectSummaryDto
            {
                UserId = u.Id,
                Name = u.Name,
                Surname = u.Surname,

                CompletedProjectsCount = _context.Projects
                    .Count(p => p.UserId == u.Id && p.Status == ProjectStatus.Completed),

                InProgressProjectsCount = _context.Projects
                    .Count(p => p.UserId == u.Id && p.Status == ProjectStatus.Published),

                LastCompletedProjectDate = _context.Projects
                    .Where(p => p.UserId == u.Id && p.Status == ProjectStatus.Completed)
                    .Max(p => (DateTime?)p.CompletedAt)
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<UserProjectSummaryDto>(items, page, pageSize, totalCount);
    }
}
