namespace Exam.App.Domain;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProjectSkill> ProjectSkills { get; set; } = [];

}
