using Exam.App.Services.Dtos;

namespace Exam.App.Services;

public interface IProjectSkillService
{
    Task<ProjectSkillDto> CreateAsync(ProjectSkillDto skillDto, string username);
    
}