using IntershipTaskManager.Application.Interfaces.ApplicationDbContext;
using IntershipTaskManager.Application.Interfaces.Repositoreis;
using IntershipTaskManager.Domain.Entities.Intership;
using IntershipTaskManager.Infrastucture.Repositoreis.GenericRepository;
using IntershipTaskManager.Interfaces.ApplicationDB.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace IntershipTaskManager.Infrastucture.Repositoreis.IntershipRepository
{
    public class IntershipRepository : GenericRepository<Intership>, IIntershipRepository
    {
        private readonly ApplicationDbContext _context;
        public IntershipRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

    }
}