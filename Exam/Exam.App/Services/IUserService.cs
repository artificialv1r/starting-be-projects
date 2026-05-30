using Exam.App.Services.Dtos;

namespace Exam.App.Services;

public interface IUserService
{
    Task<PaginatedListDto<ProfileDto>> GetAllUsersAsync(int page, int pageSize);
    Task<PaginatedListDto<UserProjectSummaryDto>> GetUsersWithProjectStatsAsync(int page, int pageSize);
}
