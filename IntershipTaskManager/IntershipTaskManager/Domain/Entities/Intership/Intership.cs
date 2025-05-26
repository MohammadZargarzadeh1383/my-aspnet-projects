using IntershipTaskManager.Domain.Entities.Common;
using IntershipTaskManager.Domain.Enums;

namespace IntershipTaskManager.Domain.Entities.Intership
{
    public class Intership : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public IntershipType Type { get; set; }
    }
}
