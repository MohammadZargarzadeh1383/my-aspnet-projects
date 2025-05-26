using IntershipTaskManager.Application.Interfaces.Repositoreis;
using IntershipTaskManager.Domain.Entities.Intership;
using IntershipTaskManager.Domain.Entities.Task;
using IntershipTaskManager.Infrastucture.Repositoreis.GenericRepository;
using IntershipTaskManager.Interfaces.ApplicationDB.ApplicationDbContext;
using System.Reflection;

namespace IntershipTaskManager.Infrastucture.Repositoreis.MissionRepository
{
    public class MissionRepository : GenericRepository<Mission>, IMissionRepository
    {
        private ApplicationDbContext _context;
        public MissionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}