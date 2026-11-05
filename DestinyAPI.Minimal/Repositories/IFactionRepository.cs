using DestinyAPI.Models;

namespace DestinyAPI.Repositories
{
    public interface IFactionRepository
    {
        Task<List<Faction>> GetAllAsync();
        Task<Faction?> GetByIdAsync(int id);
        Task<Faction> AddAsync(Faction faction);
        Task UpdateAsync(Faction faction);
        Task DeleteAsync(int id);
    }
}
