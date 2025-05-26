using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Application.Interfaces.Repositories;
using WebApplication2.Application.Models.Dto.Notion;
using WebApplication2.Domain.Entities.Notion;
using WebApplication2.Domain.Entities.User;
using WebApplication2.Infrastucture.Repositories.NotionRepository;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotionController : ControllerBase
    {
        private readonly INotionRepository _notionRepository;

        public NotionController(INotionRepository notionRepository)
        {
            _notionRepository = notionRepository;
        }

        [HttpPost(Name = "{id}")]
        public async Task<ActionResult<Notion>> CreateNotion(CreatNotionDto notionDto)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var notion = new Notion()
            {
                Title = notionDto.Title,
                Description = notionDto.Description,
                CreatedBy = "system",
                CreatedAt = DateTime.Now,
                Location = "اصفهان",
                UserRef = int.Parse(userid)
            };
            var createnotion = await _notionRepository.Create(notion);
            return Ok(createnotion);
        }

        [HttpGet(Name = "GetAllNotion")]
        public async Task<ActionResult<List<ShowNotionDto>>> GetAllNotion()
        {
            var getallnotion = await _notionRepository.GetAll().Select(x => new ShowNotionDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreatedBy = x.CreatedBy
            }
            ).ToListAsync();
            return Ok(getallnotion);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notion>> GetUser(int id)
        {
            var notion = await _notionRepository.GetById(id);
            var result = new ShowNotionDto
            {
                Id = id,
                Title = notion.Title,
                Description = notion.Description,
                CreatedBy = "system"
            };
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Notion>> UpdateNotion([FromQuery]int id, [FromBody] CreatNotionDto notiondto)
        {
            //var userid = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var notion = await _notionRepository.GetById(/*int.Parse(userid)*/id);
            if (notion == null)
            {
                return NotFound();
            }
            else
            {
                notion.Title = notiondto.Title;
                notion.Description = notiondto.Description;
                notion.UpdatedAt = DateTime.Now;
                var updatenotion = await _notionRepository.Update(notion);
                return Ok(updatenotion);
            };
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNotion()
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var notion = await _notionRepository.GetById(int.Parse(userid));
            if (notion == null)
            {
                return NotFound();
            }
            else
            {
                var deletenotion = await _notionRepository.Delete(notion);
                return Ok();
            };
        }
    }
}

