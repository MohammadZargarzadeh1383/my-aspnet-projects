namespace TAMRIN.Domain.Entites.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        //public AddAction AppAction { get; set; } = AddAction.Active;

    }
}
