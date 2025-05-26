using IntershipTaskManager.Application.Interfaces.Repositoreis;
using IntershipTaskManager.Application.Models.Dto.Intership;
using IntershipTaskManager.Application.Models.Dto.IntershipTask;
using IntershipTaskManager.Domain.Entities.Intership;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace IntershipTaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class IntershipTaskController : ControllerBase
{
    private readonly ILogger<IntershipTaskController> _logger;
    private readonly IGenericRepository<IntershipTask> _genericRepository;

    public IntershipTaskController(ILogger<IntershipTaskController> logger, IGenericRepository<IntershipTask> genericRepository)
    {
        _logger = logger;
        _genericRepository = genericRepository;
    }
    [HttpPost("CreateInttershipTask")]
    public async Task<ActionResult<IntershipTask>> CreateInttershipTask([FromQuery] CreateIntershipTaskDto createIntershipTaskDto)
    {
        var intershipTask = new IntershipTask
        {
            MissionRef = createIntershipTaskDto.MissionRef,
            IntershipRef = createIntershipTaskDto.IntershipRef,
            Link = createIntershipTaskDto.Link,
        };
        var createIntershipTask = await _genericRepository.Creat(intershipTask);
        if (createIntershipTask > 0)
        {
            return Ok(createIntershipTask);
            
        }
        return BadRequest();
    }

    [HttpGet("GetIntershipTasks")]
    public async Task<ActionResult<List<IntershipTask>>> GetIntershipTasks()
    {
        var getAll = _genericRepository.GetAllUser().Select(m => new IntershipTask
        {
            Link = m.Link,
        }).ToList();
        return Ok(getAll);
    }

    [HttpPost("GetIntershipTaskById")]
    public async Task<ActionResult<IntershipTask>> GetIntershipTaskById([FromQuery] int Id)
    {
        var getIntership = await _genericRepository.GetById(Id);
        if (getIntership == null)
        {
            return BadRequest();
        }
        return Ok(getIntership);
    }
    [HttpPut("IntershipTaskUpdate")]
    public async Task<ActionResult<bool>> IntershipTaskUpdate([FromQuery] int Id, [FromQuery] int IdIntership, [FromQuery] int IdMission, [FromBody] ShowIntershipTaskDto showIntershipTaskDto)
    {
        var FindIntershipTask = await _genericRepository.GetById(Id);
        FindIntershipTask.Link = showIntershipTaskDto.Link;
        FindIntershipTask.MissionRef = IdMission;
        FindIntershipTask.IntershipRef = IdIntership;
        var save = await _genericRepository.Update(FindIntershipTask);
        if (save > 0) { return Ok(true); }
        else
        {
            return BadRequest(false);
        }
    }
    [HttpDelete("IntershipTaskDelete")]
    public async Task<ActionResult<bool>> IntershipTaskDelete(int Id)
    {
        var FindIntership = await _genericRepository.GetById(Id);
        if (FindIntership == null)
        {
            return BadRequest();
        }
        await _genericRepository.Delete(FindIntership);
        return true;
    }
}
