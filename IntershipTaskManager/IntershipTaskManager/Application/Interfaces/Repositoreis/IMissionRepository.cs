using IntershipTaskManager.Domain.Entities.Intership;
using IntershipTaskManager.Domain.Entities.Task;

namespace IntershipTaskManager.Application.Interfaces.Repositoreis;

public interface IMissionRepository
{
    public Task<int> Creat(Mission mission);
    public Task<bool> Delete(Mission mission);
    public Task<int> Update(Mission mission);
    public Task<Mission> GetById(int id);
    public IQueryable<Mission> GetAllUser();
}
