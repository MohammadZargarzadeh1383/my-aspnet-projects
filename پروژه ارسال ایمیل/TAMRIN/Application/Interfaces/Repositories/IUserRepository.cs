using TAMRIN.Domain.Entites.User;

namespace TAMRIN.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public  void Emaillogin(User user);
        public void SendEmail(string email, string body);

    }
}
