using DestinyAPI.Data;
using DestinyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DestinyAPI.Repositories
{
    public class EfCharacterRepository : ICharacterRepository
    {
        private readonly DestinyDbContext _context;

        public EfCharacterRepository(DestinyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character?> GetByIdAsync(int id)
        {
            return await _context.Characters.FindAsync(id);
        }

        public async Task<Character> AddAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task UpdateAsync(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Characters.FindAsync(id);
            if (entity != null)
            {
                _context.Characters.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
