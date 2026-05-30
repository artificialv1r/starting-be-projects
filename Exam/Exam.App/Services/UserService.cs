using AutoMapper;
using Exam.App.Domain.Repositories;
using Exam.App.Services.Dtos;
using Exam.App.Services.Exceptions;

namespace Exam.App.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedListDto<ProfileDto>> GetAllUsersAsync(int page, int pageSize)
    {
        var paginatedUsers = await _userRepository.GetAllAsync(page, pageSize);

        var profileDtos = _mapper.Map<List<ProfileDto>>(paginatedUsers.Items);

        return new PaginatedListDto<ProfileDto>(profileDtos, paginatedUsers.Page, paginatedUsers.PageSize, paginatedUsers.TotalCount);
    }

    public async Task<PaginatedListDto<UserProjectSummaryDto>> GetUsersWithProjectStatsAsync(int page, int pageSize)
    {
        var paginatedUsers = await _userRepository.GetUsersWithProjectStatsAsync(page, pageSize);

        var dtos = _mapper.Map<List<UserProjectSummaryDto>>(paginatedUsers.Items);

        return new PaginatedListDto<UserProjectSummaryDto>(
            dtos,
            paginatedUsers.Page,
            paginatedUsers.PageSize,
            paginatedUsers.TotalCount
        );
    }
}
