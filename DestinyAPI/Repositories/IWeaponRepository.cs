using DestinyAPI.Models;

namespace DestinyAPI.Repositories
{
    public interface IWeaponRepository
    {
        Task<IEnumerable<Weapon>> GetAllAsync();
        Task<Weapon?> GetByIdAsync(int id);
        Task<Weapon> CreateAsync(Weapon weapon);
        Task<bool> UpdateAsync(Weapon weapon);
        Task<bool> DeleteAsync(int id);
    }
}
