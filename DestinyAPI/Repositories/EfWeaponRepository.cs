using DestinyAPI.Data;
using DestinyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DestinyAPI.Repositories
{
    public class EfWeaponRepository : IWeaponRepository
    {
        private readonly DestinyDbContext _context;

        public EfWeaponRepository(DestinyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Weapon>> GetAllAsync() =>
            await _context.Weapons.Include(w => w.Faction).ToListAsync();

        public async Task<Weapon?> GetByIdAsync(int id) =>
            await _context.Weapons.Include(w => w.Faction).FirstOrDefaultAsync(w => w.Id == id);

        public async Task<Weapon> CreateAsync(Weapon weapon)
        {
            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();
            return weapon;
        }

        public async Task<bool> UpdateAsync(Weapon weapon)
        {
            if (!_context.Weapons.Any(w => w.Id == weapon.Id)) return false;

            _context.Weapons.Update(weapon);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null) return false;

            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
