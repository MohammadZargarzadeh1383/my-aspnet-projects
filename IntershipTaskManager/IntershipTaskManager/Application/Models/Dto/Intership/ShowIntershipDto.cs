using IntershipTaskManager.Domain.Enums;

namespace IntershipTaskManager.Application.Models.Dto.Intership
{
    public class ShowIntershipDto
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public IntershipType Type { get; set; }
    }
}
