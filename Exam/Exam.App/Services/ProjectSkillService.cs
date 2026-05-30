using AutoMapper;
using Exam.App.Domain;
using Exam.App.Domain.Repositories;
using Exam.App.Services.Dtos;
using Exam.App.Services.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Exam.App.Services;

public class ProjectSkillService : IProjectSkillService
{
    private readonly IProjectSkillRepository _projectSkillRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public ProjectSkillService(IProjectSkillRepository projectSkillRepository, IProjectRepository projectRepository,
        UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _projectSkillRepository = projectSkillRepository;
        _projectRepository = projectRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ProjectSkillDto> CreateAsync(ProjectSkillDto dto, string username)
    {
        var project = await _projectRepository.GetByIdAsync(dto.ProjectId);
        if (project == null)
        {
            throw new NotFoundException(dto.ProjectId);
        }

        if (!project.CanAddSkill())
        {
            throw new InvalidOperationException();
        }

        var user = await _userManager.FindByNameAsync(username);
        
        if (project.UserId != user.Id)
        {
            throw new UnauthorizedAccessException();
        }

        var newProjectSkill = new ProjectSkill
        {
            Description = dto.Description,
            ProjectId = dto.ProjectId,
            SkillId = dto.SkillId
        };

        var created = await _projectSkillRepository.CreateAsync(newProjectSkill);
        return _mapper.Map<ProjectSkillDto>(created);
    }
}