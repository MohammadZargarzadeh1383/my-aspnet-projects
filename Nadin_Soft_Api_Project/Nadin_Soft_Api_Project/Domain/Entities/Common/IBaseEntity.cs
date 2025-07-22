using Nadin_Soft_Api_Project.Domain.Enums.AppAction;

namespace Nadin_Soft_Api_Project.Domain.Entities.Common
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public AppAction AppAction { get; set; }
    }
}
