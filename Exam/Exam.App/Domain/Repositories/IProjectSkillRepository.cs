namespace Exam.App.Domain.Repositories;

public interface IProjectSkillRepository
{
    Task<List<ProjectSkill>> GetAllAsync();
    Task<ProjectSkill> CreateAsync(ProjectSkill skill);
}