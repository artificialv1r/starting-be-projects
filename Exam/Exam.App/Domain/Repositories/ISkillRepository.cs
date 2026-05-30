namespace Exam.App.Domain.Repositories;

public interface ISkillRepository
{
    Task<List<Skill>> GetAllAsync();
}