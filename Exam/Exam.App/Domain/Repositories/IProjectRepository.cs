using Exam.App.Services.Dtos;

namespace Exam.App.Domain.Repositories;

public interface IProjectRepository
{
    Task<Project> CreateAsync(Project project);
    Task<Project?> GetByIdAsync(int id);
    Task<List<Project>> GetByUserIdAsync(string userId);
    Task<List<Project>> GetOwnedByUserIdAsync(string userId);
    Task UpdateAsync(Project project);
    Task DeleteAsync(int id);
    Task<List<Project>> GetFilteredByUserIdAsync(string userId, ProjectStatus status);
    
}
