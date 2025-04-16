using TAMRIN.Domain.Entites.Common;

namespace TAMRIN.Domain.Entites.User
{
    public class User : BaseEntity
    {
        public int Name { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
    }
}
