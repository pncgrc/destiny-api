using DestinyAPI.Models;

namespace DestinyAPI.Repositories
{
    public class InMemoryWeaponRepository : IWeaponRepository
    {
        private readonly List<Weapon> _weapons = new()
        {
            new Weapon { Id = 1, Name = "Pulse Rifle", FactionId = 1, Damage = 123.45m },
            new Weapon { Id = 2, Name = "Void Sword", FactionId = 2, Damage = 87.50m },
            new Weapon { Id = 3, Name = "Shock Cannon", FactionId = 3, Damage = 205.00m }
        };

        public Task<IEnumerable<Weapon>> GetAllAsync() => Task.FromResult<IEnumerable<Weapon>>(_weapons);

        public Task<Weapon?> GetByIdAsync(int id) =>
            Task.FromResult(_weapons.FirstOrDefault(w => w.Id == id));

        public Task<Weapon> CreateAsync(Weapon weapon)
        {
            weapon.Id = _weapons.Max(w => w.Id) + 1;
            _weapons.Add(weapon);
            return Task.FromResult(weapon);
        }

        public Task<bool> UpdateAsync(Weapon weapon)
        {
            var index = _weapons.FindIndex(w => w.Id == weapon.Id);
            if (index == -1) return Task.FromResult(false);
            _weapons[index] = weapon;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var weapon = _weapons.FirstOrDefault(w => w.Id == id);
            if (weapon == null) return Task.FromResult(false);
            _weapons.Remove(weapon);
            return Task.FromResult(true);
        }
    }
}
