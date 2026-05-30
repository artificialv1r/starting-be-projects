using AutoMapper;
using Exam.App.Domain;
using Exam.App.Domain.Repositories;
using Exam.App.Services;
using Exam.App.Services.Dtos;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Exam.Tests.Services;

public class ManageProjectsTests
{
    [Fact]
    public async Task Delete_project_should_delete_only_when_user_is_owner()
    {
        // Arrange
        var projectRepo = new Mock<IProjectRepository>();
        var userManager = CreateUserManager();
        var mapper = new Mock<IMapper>();

        var user = new ApplicationUser
        {
            Id = "user-1",
            UserName = "stefan",
            Name="Stefan",
            Surname = "Gospic"
        };

        var project = new Project
        {
            Id = 10,
            UserId = "user-1"
        };

        userManager
            .Setup(u => u.FindByNameAsync(user.UserName))
            .ReturnsAsync(user);

        projectRepo
            .Setup(p => p.GetByIdAsync(project.Id))
            .ReturnsAsync(project);
        
        var service = new ProjectService(
            projectRepo.Object,
            mapper.Object,
            userManager.Object
        );

        // Act
        await service.DeleteAsync(10, "stefan");

        // Assert
        projectRepo.Verify(r => r.DeleteAsync(10), Times.Once);
    }

    [Fact]
    public async Task Delete_project_should_throw_when_user_is_not_owner()
    {
        // Arrange
        var projectRepo = new Mock<IProjectRepository>();
        var userManager = CreateUserManager();
        var mapper = new Mock<IMapper>();

        var user = new ApplicationUser
        {
            Id = "user-1",
            UserName = "stefan",
            Name = "Stefan",
            Surname = "Gospic"
        };

        var project = new Project
        {
            Id = 10,
            UserId = "user-2"
        };

        projectRepo
            .Setup(r => r.GetByIdAsync(project.Id))
            .ReturnsAsync(project);

        userManager
            .Setup(u => u.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        var service = new ProjectService(
            projectRepo.Object,
            mapper.Object,
            userManager.Object
        );

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.DeleteAsync(10, "stefan")
        );
    }

    [Fact]
    public async Task Update_project_should_throw_when_project_is_completed()
    {
        // Arrange
        var projectRepo = new Mock<IProjectRepository>();
        var userManager = CreateUserManager();
        var mapper = new Mock<IMapper>();

        var user = new ApplicationUser
        {
            Id = "user-2",
            UserName = "stefan",
            Name = "Stefan",
            Surname = "Gospic"
        };

        var project = new Project
        {
            Id = 10,
            UserId = "user-1",
            Status = ProjectStatus.Completed
        };

        var dto = new ProjectDto
        {
            Name = "New name",
            Description = "New desc",
            Status = ProjectStatus.Published
        };

        projectRepo
            .Setup(r => r.GetByIdAsync(project.Id))
            .ReturnsAsync(project);

        userManager
            .Setup(u => u.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        var service = new ProjectService(
            projectRepo.Object,
            mapper.Object,
            userManager.Object
        );

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.UpdateAsync(10, dto, "stefan")
        );
    }

    [Fact]
    public async Task Update_project_should_update_when_user_is_owner()
    {
        // Arrange
        var projectRepo = new Mock<IProjectRepository>();
        var userManager = CreateUserManager();
        var mapper = new Mock<IMapper>();

        var user = new ApplicationUser
        {
            Id = "user-1",
            UserName = "stefan",
            Name="Stefan",
            Surname = "Gospic"
        };

        var project = new Project
        {
            Id = 10,
            UserId = "user-1",
            Status = ProjectStatus.Draft,
            Name = "Old name",
            Description = "Old desc"
        };

        var dto = new ProjectDto
        {
            Name = "Updated",
            Description = "Updated desc",
            Status = ProjectStatus.Published,
            StartedAt = DateTime.UtcNow,
            CompletedAt = null
        };

        var mappedProject = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            Status = dto.Status
        };

        projectRepo
            .Setup(r => r.GetByIdAsync(10))
            .ReturnsAsync(project);

        projectRepo
            .Setup(r => r.UpdateAsync(project))
            .Returns(Task.CompletedTask);

        userManager
            .Setup(u => u.FindByNameAsync("stefan"))
            .ReturnsAsync(user);

        mapper
            .Setup(m => m.Map<ProjectDto>(It.IsAny<Project>()))
            .Returns(new ProjectDto
            {
                Name = project.Name,
                Description = project.Description,
                Status = project.Status
            });

        var service = new ProjectService(
            projectRepo.Object,
            mapper.Object,
            userManager.Object
        );

        // Act
        var result = await service.UpdateAsync(10, dto, "stefan");

        // Assert
        projectRepo.Verify(r => r.UpdateAsync(It.Is<Project>(p =>
            p.Name == dto.Name &&
            p.Description == dto.Description &&
            p.Status == dto.Status &&
            p.StartedAt == dto.StartedAt &&
            p.CompletedAt == dto.CompletedAt
        )), Times.Once);

        mapper.Verify(m => m.Map<ProjectDto>(project), Times.Once);
    }

    private static Mock<UserManager<ApplicationUser>> CreateUserManager()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();

        return new Mock<UserManager<ApplicationUser>>(
            store.Object,
            null, null, null, null, null, null, null, null
        );
    }
}
