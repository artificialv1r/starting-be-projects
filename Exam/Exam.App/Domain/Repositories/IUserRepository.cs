using Exam.App.Services.Dtos;

namespace Exam.App.Domain.Repositories;

public interface IUserRepository
{
    Task<PaginatedList<ApplicationUser>> GetAllAsync(int page, int pageSize);
    Task<PaginatedList<UserProjectSummaryDto>> GetUsersWithProjectStatsAsync(int page, int pageSize);
}
