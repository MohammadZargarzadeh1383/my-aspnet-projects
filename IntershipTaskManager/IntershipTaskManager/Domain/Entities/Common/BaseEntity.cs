using IntershipTaskManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IntershipTaskManager.Domain.Entities.Common;

public class BaseEntity
{
    [Key]
    public uint Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string? UpdatedBy { get; set; }
    public AppActionType AppAction { get; set; } = AppActionType.Active;
}
