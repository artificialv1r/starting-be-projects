using Exam.App.Domain;
using Exam.App.Domain.Repositories;

namespace Exam.App.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;

    public SkillService(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<List<Skill>> GetAllAsync()
    {
        return await _skillRepository.GetAllAsync();
    }
}