using Exam.App.Domain;
using Exam.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exam.App.Controllers;

[Route("api/skills")]
[ApiController]
public class SkillsController : ControllerBase
{
    private readonly ISkillService _skillService;
    
    public SkillsController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Skill>>> GetAllAsync()
    {
        return Ok(await _skillService.GetAllAsync());
    }
}