using Exam.App.Domain;
using Exam.App.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Infrastructure.Database.Repositories;

public class ProjectSkillRepository : IProjectSkillRepository
{
    private readonly AppDbContext _context;

    public ProjectSkillRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectSkill>> GetAllAsync()
    {
        return await _context.ProjectSkills.ToListAsync();
    }

    public async Task<ProjectSkill> CreateAsync(ProjectSkill skill)
    {
        _context.ProjectSkills.Add(skill);
        await _context.SaveChangesAsync();
        return skill;
    }
}