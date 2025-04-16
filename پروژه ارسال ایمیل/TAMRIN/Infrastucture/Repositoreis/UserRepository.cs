using System.Net;
using System.Net.Http;
using System.Net.Mail;
using TAMRIN.Application.Interfaces.Repositories;
using TAMRIN.Domain.Entites.User;
using TAMRIN.Infrastucture.ApplicationDb.ApplicationDbContext;

namespace TAMRIN.Infrastucture.Repositoreis
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext _dbcontext)
        {
            _context = _dbcontext;
        }
        public async void Emaillogin(User user)
        {
            string Code = new Random().Next(999, 10000).ToString();
            SendEmail(user.Email, $"yout password is {Code}");

        }
        public void SendEmail(string toEmail, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                //Port = 25,
                Credentials = new NetworkCredential("testapilogin999@gmail.com", "app password"), 
                EnableSsl = true,
            };

            smtpClient.Send("testapilogin999@gmail.com", toEmail, "Verification Code", body);
        }

    }
}
