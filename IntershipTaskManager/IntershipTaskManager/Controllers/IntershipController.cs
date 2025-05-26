using IntershipTaskManager.Application.Interfaces.Repositoreis;
using IntershipTaskManager.Application.Models;
using IntershipTaskManager.Application.Models.Dto.Intership;
using IntershipTaskManager.Application.Models.Dto.IntershipTask;
using IntershipTaskManager.Domain.Entities.Intership;
using IntershipTaskManager.Domain.Entities.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace IntershipTaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class IntershipController : ControllerBase
{
    private readonly ILogger<IntershipController> _logger;
    private readonly IGenericRepository<Intership> _genericRepository;

    public IntershipController(ILogger<IntershipController> logger, IGenericRepository<Intership> genericRepository)
    {
        _logger = logger;
        _genericRepository = genericRepository;
    }

    [HttpPost("CreateInttership")]
    public async Task<ActionResult<bool>> CreateInttership(ShowIntershipDto showIntershipDto)
    {
        var intership = new Intership
        {
            FirstName = showIntershipDto.FirstName,
            LastName = showIntershipDto.LastName,
            Gender = showIntershipDto.Gender,
            Type = showIntershipDto.Type,

        };
        var createIntershipTask = await _genericRepository.Creat(intership);
        if (createIntershipTask > 0)
        {
            return Ok(true);
            
        }
        return BadRequest(false);
    }

    [HttpGet("GetInterships")]
    public async Task<ActionResult<List<ShowIntershipDto>>> GetInterships()
    {
        var getAll = _genericRepository.GetAllUser().Select(m => new ShowIntershipDto
        {
            FirstName = m.FirstName,
            LastName = m.LastName,
            Gender = m.Gender,
            Type = m.Type,
        }).ToList();
        return Ok(getAll);
    }

    [HttpPost("GetIntershipsById")]
    public async Task<ActionResult<ShowIntershipDto>> GetInterships([FromQuery] int Id)
    {
        var getIntership = await _genericRepository.GetById(Id);
        if (getIntership == null)
        {
            return BadRequest();
        }
        return Ok(getIntership);
    }
    [HttpPut("IntershipUpdate")]
    public async Task<ActionResult<bool>> Update([FromQuery] int Id, [FromBody] ShowIntershipDto showIntershipDto)
    {
        var FindIntership = await _genericRepository.GetById(Id);
        FindIntership.FirstName = showIntershipDto.FirstName;
        FindIntership.LastName = showIntershipDto.LastName;
        FindIntership.Type = showIntershipDto.Type;
        FindIntership.Gender = showIntershipDto.Gender;
        var save = await _genericRepository.Update(FindIntership);
        if (save > 0) { return Ok(true); }
        else
        {
            return BadRequest(false);
        }
    }
    [HttpDelete("IntershipDelete")]
    public async Task<ActionResult<bool>> Delete(int Id)
    {
        var FindIntership = await _genericRepository.GetById(Id);
        if (FindIntership == null)
        {
            return BadRequest(false);
        }
        await _genericRepository.Delete(FindIntership);
        return true;
    }
}

