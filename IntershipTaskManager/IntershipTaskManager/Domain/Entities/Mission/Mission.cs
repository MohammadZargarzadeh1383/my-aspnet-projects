using IntershipTaskManager.Domain.Entities.Common;
using IntershipTaskManager.Domain.Enums;

namespace IntershipTaskManager.Domain.Entities.Task;

public class Mission : BaseEntity
{
    public LeaderType LeaderRef { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ExpireDate { get; set; }
}
