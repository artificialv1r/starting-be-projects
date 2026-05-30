using Exam.App.Domain;
using Exam.App.Domain.Repositories;
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
}
