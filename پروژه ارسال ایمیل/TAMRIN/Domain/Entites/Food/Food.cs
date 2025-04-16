using TAMRIN.Domain.Entites.Common;

namespace TAMRIN.Domain.Entites.Food
{
    public class Food : BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int MyProperty1 { get; set; }
    }
}
