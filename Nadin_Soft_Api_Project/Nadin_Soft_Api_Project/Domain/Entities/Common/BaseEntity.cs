using Nadin_Soft_Api_Project.Domain.Enums.AppAction;

namespace Nadin_Soft_Api_Project.Domain.Entities.Common
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public AppAction AppAction { get; set; } = AppAction.Active;

    }
}
