using IntershipTaskManager.Application.Interfaces.Repositoreis;
using IntershipTaskManager.Application.Models;
using IntershipTaskManager.Application.Models.Dto.IntershipTask;
using IntershipTaskManager.Application.Models.Dto.Mission;
using IntershipTaskManager.Domain.Entities.Intership;
using IntershipTaskManager.Domain.Entities.Task;
using Microsoft.AspNetCore.Mvc;

namespace IntershipTaskManager.Controllers;
[ApiController]
[Route("[controller]")]
public class MissionController : ControllerBase
{
    private readonly ILogger<MissionController> _logger;
    private readonly IGenericRepository<Mission> _genericRepository;

    public MissionController(ILogger<MissionController> logger, IGenericRepository<Mission> genericRepository)
    {
        _logger = logger;
        _genericRepository = genericRepository;
    }
    [HttpPost("CreateInttershipTask")]
    public async Task<ActionResult<bool>> CreateInttershipTask(CreateMissionDto createMissionDto)
    {
        var Mission = new Mission
        {
             LeaderRef = createMissionDto.LeaderRef,
             Title = createMissionDto.Title,
             Description= createMissionDto.Description,
             ExpireDate = createMissionDto.ExpireDate,
        };
        var createIntershipTask = await _genericRepository.Creat(Mission);
        if (createIntershipTask > 0) { return Ok(true); }
        else
        {
            return BadRequest(false);
        }
    }

    [HttpGet("GetIntershipTasks")]
    public async Task<ActionResult<List<Mission>>> GetIntershipTasks()
    {
        var getAll = _genericRepository.GetAllUser().Select(m => new Mission
        {
            LeaderRef = m.LeaderRef,
            Title = m.Title,
            Description = m.Description,
            ExpireDate = m.ExpireDate,
        }).ToList();
        return Ok(getAll);
    }

    [HttpPost("GetIntershipTaskById")]
    public async Task<ActionResult<Mission>> GetIntershipTaskById([FromQuery] int Id)
    {
        var getMission = await _genericRepository.GetById(Id);
        if (getMission == null)
        {
            return BadRequest();
        }
        return Ok(getMission);
    }
    [HttpPut("IntershipTaskUpdate")]
    public async Task<ActionResult<bool>> IntershipTaskUpdate([FromQuery] int Id,[FromQuery] ShowMissionDto showMissionDto)
    {
        var FindIntershipTask = await _genericRepository.GetById(Id);
        FindIntershipTask.Title = showMissionDto.Title;
        FindIntershipTask.LeaderRef = showMissionDto.LeaderRef;
        FindIntershipTask.ExpireDate = showMissionDto.ExpireDate;
        FindIntershipTask.Description = showMissionDto.Description;
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
























































//[ApiController]
//[Route("[controller]")]
//public class MissionController : ControllerBase
//{
//    private readonly ILogger<MissionController> _logger;
//    private readonly IGenericRepository<Mission> _genericRepository;

//    public MissionController(ILogger<MissionController> logger, IMissionRepository missionRepository, IGenericRepository<Mission> genericRepository)
//    {
//        _logger = logger;
//        _genericRepository = genericRepository;
//    }

//    [HttpGet("GetMissions")]
//    public async Task<ActionResult<IQueryable<Mission>>> GetMissions()
//    {
//        var getAll = _genericRepository.GetAllUser();

//        return Ok(getAll);
//    }

//    [HttpGet("GetMissionById/{id}")]
//    public async Task<ActionResult<Mission>> GetMissionById(int id)
//    {
//        var getAll = await _genericRepository.GetById(id);

//        return Ok(getAll);
//    }

//    [HttpPost("CreateMission")]
//    public async Task<ActionResult<bool>> CreateMission([FromBody] CreateMissionDto mission)
//    {
//        var create = await _genericRepository.Creat(new Mission
//        {
//            LeaderRef = (Domain.Enums.LeaderType)mission.LeaderRef,
//            Title = mission.Title,
//            Description = mission.Description,
//            ExpireDate = mission.ExpireDate
//        });
//        if (create > 0) return Ok(true);
//        else return BadRequest(false);
//    }

//}
