using Exam.App.Domain;

namespace Exam.App.Services;

public interface ISkillService
{
    Task<List<Skill>> GetAllAsync();
}