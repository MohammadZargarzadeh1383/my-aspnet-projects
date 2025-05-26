using IntershipTaskManager.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntershipTaskManager.Domain.Entities.Intership;

public class IntershipTask : BaseEntity
{
    [ForeignKey("MissionRef")]
    public int MissionRef { get; set; }
    [ForeignKey("IntershipRef")]
    public int IntershipRef { get; set; }
    public string Link { get; set; }

    public virtual IntershipTaskManager.Domain.Entities.Intership.Intership Intership { get; set; }
    public virtual IntershipTaskManager.Domain.Entities.Task.Mission Mission { get; set; }
}
