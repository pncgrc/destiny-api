using DestinyAPI.Models;
using System.Collections.Concurrent;

namespace DestinyAPI.Repositories;

public class InMemoryWeaponRepository : IWeaponRepository
{
    private readonly ConcurrentDictionary<int, Weapon> _weapons = new();

    public InMemoryWeaponRepository()
    {
        _weapons.TryAdd(1, new Weapon { Id = 1, Name = "Pulse Rifle", FactionId = 1 });
        _weapons.TryAdd(2, new Weapon { Id = 2, Name = "Void Sword", FactionId = 2 });
    }

    public Task<IEnumerable<Weapon>> GetAllAsync() =>
        Task.FromResult(_weapons.Values.AsEnumerable());

    public Task<Weapon?> GetByIdAsync(int id) =>
        Task.FromResult(_weapons.TryGetValue(id, out var weapon) ? weapon : null);

    public Task<Weapon> AddAsync(Weapon weapon)
    {
        // Generate new Id if not set or already exists
        if (weapon.Id == 0 || _weapons.ContainsKey(weapon.Id))
        {
            weapon.Id = _weapons.Keys.DefaultIfEmpty(0).Max() + 1;
        }
        _weapons.TryAdd(weapon.Id, weapon);
        return Task.FromResult(weapon);
    }

    public Task<bool> UpdateAsync(Weapon weapon)
    {
        if (!_weapons.ContainsKey(weapon.Id))
            return Task.FromResult(false);

        _weapons[weapon.Id] = weapon;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var removed = _weapons.TryRemove(id, out _);
        return Task.FromResult(removed);
    }
}
