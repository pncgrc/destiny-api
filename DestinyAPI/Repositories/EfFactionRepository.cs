using DestinyAPI.Data;
using DestinyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DestinyAPI.Repositories
{
    public class EfFactionRepository : IFactionRepository
    {
        private readonly DestinyDbContext _context;

        public EfFactionRepository(DestinyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Faction>> GetAllAsync() =>
            await _context.Factions.ToListAsync();

        public async Task<Faction?> GetByIdAsync(int id) =>
            await _context.Factions.FindAsync(id);

        public async Task<Faction> AddAsync(Faction faction)
        {
            _context.Factions.Add(faction);
            await _context.SaveChangesAsync();
            return faction; // ID wordt hier door de DB gegenereerd
        }

        public async Task UpdateAsync(Faction faction)
        {
            _context.Entry(faction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var faction = await _context.Factions.FindAsync(id);
            if (faction != null)
            {
                _context.Factions.Remove(faction);
                await _context.SaveChangesAsync();
            }
        }
    }

}
