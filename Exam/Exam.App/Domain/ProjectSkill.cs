namespace Exam.App.Domain;

public class ProjectSkill
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int SkillId { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public Skill Skill { get; set; }
}