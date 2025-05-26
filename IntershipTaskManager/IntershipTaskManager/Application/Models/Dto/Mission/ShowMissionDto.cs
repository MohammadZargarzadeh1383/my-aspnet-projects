using IntershipTaskManager.Domain.Enums;

namespace IntershipTaskManager.Application.Models.Dto.Mission
{
    public class ShowMissionDto
    {
        public LeaderType LeaderRef { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
