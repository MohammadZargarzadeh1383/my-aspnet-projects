//using IntershipTaskManager.Application.Interfaces;
//using IntershipTaskManager.Application.Interfaces.ApplicationDbContext;
//using IntershipTaskManager.Domain.Entities.Intership;
//using IntershipTaskManager.Domain.Entities.Task;
//using IntershipTaskManager.Interfaces.ApplicationDB.ApplicationDbContext;
//using Microsoft.EntityFrameworkCore;

//namespace IntershipTaskManager.Infrastucture.Repositoreis.MissionRepository;

//public class MissionRepository : IMissionRepository
//{
//    private readonly ApplicationDbContext _context;
//    public MissionRepository(ApplicationDbContext _dbcontext)
//    {
//        _context = _dbcontext;
//    }

//    public async Task<List<Mission>> GetMissions()
//    {
//        var missions = await _context.Missions.Where(m => m.AppAction == Domain.Enums.AppActionType.Active).ToListAsync();
//        return missions;
//    }

//    public async Task<List<Intership>> GetInterships()
//    {
//        var Interships = await _context.Interships.Where(m => m.AppAction == Domain.Enums.AppActionType.Active).ToListAsync();
//        return Interships;
//    }

//    public async Task<Mission> GetMissionById(uint id)
//    {
//        var mission = await _context.Missions.FirstOrDefaultAsync(m => m.Id == id && m.AppAction == Domain.Enums.AppActionType.Active);

//        return mission;
//    }
//    public async Task<int> CreateMission(Mission mission)
//    {
//        await _context.Missions.AddAsync(mission);
//        var save = await _context.SaveChangesAsync();
//        return save;
//    }

//    public async Task<int> CreateIntershipTask(IntershipTask intershipTask)
//    {
//        await _context.IntershipTasks.AddAsync(intershipTask);
//        var save = await _context.SaveChangesAsync();
//        return save;
//    }
//}
