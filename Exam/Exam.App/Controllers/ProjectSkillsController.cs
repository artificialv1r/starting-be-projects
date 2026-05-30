using System.Security.Claims;
using Exam.App.Services;
using Exam.App.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.App.Controllers;

[Route("api/project-skills")]
[ApiController]
[Authorize]
public class ProjectSkillsController : ControllerBase
{
    private readonly IProjectSkillService _projectSkillService;

    public ProjectSkillsController(IProjectSkillService projectSkillService)
    {
        _projectSkillService = projectSkillService;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectSkillDto>> Create([FromBody] ProjectSkillDto dto)
    {
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _projectSkillService.CreateAsync(dto, username);
        return Ok(result);
    }
}