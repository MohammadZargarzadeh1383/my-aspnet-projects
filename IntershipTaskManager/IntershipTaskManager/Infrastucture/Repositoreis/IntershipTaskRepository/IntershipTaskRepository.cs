using IntershipTaskManager.Application.Interfaces.Repositoreis;
using IntershipTaskManager.Domain.Entities.Intership;
using IntershipTaskManager.Infrastucture.Repositoreis.GenericRepository;
using IntershipTaskManager.Interfaces.ApplicationDB.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace IntershipTaskManager.Infrastucture.Repositoreis.IntershipTaskRepository
{
    public class IntershipTaskRepository : GenericRepository<IntershipTask>, IIntershipTaskRepository
    {
        private readonly ApplicationDbContext _context;
        public IntershipTaskRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

    }
}
