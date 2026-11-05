using DestinyAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DestinyAPI.Repositories
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllAsync();
        Task<Character?> GetByIdAsync(int id);
        Task<Character> AddAsync(Character character);
        Task UpdateAsync(Character character);
        Task DeleteAsync(int id);
    }
}
