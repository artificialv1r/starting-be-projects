namespace Exam.App.Services.Dtos;

public class UserProjectSummaryDto
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public int CompletedProjectsCount { get; set; }
    public int InProgressProjectsCount { get; set; }
    public DateTime? LastCompletedProjectDate { get; set; }
}