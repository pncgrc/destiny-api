using DestinyAPI.Models;

namespace DestinyAPI.Repositories
{
    public class InMemoryFactionRepository : IFactionRepository
    {
        private readonly List<Faction> _factions = new()
    {
        new Faction { Id = 1, Name = "Humanity" },
        new Faction { Id = 2, Name = "Hive" },
        new Faction { Id = 3, Name = "Fallen" }
    };

        private int _nextId = 4;

        public Task<List<Faction>> GetAllAsync() => Task.FromResult(_factions);

        public Task<Faction?> GetByIdAsync(int id) =>
            Task.FromResult(_factions.FirstOrDefault(f => f.Id == id));

        public Task<Faction> AddAsync(Faction faction)
        {
            faction.Id = _nextId++;
            _factions.Add(faction);
            return Task.FromResult(faction);
        }

        public Task UpdateAsync(Faction faction)
        {
            var index = _factions.FindIndex(f => f.Id == faction.Id);
            if (index != -1)
                _factions[index] = faction;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var faction = _factions.FirstOrDefault(f => f.Id == id);
            if (faction != null)
                _factions.Remove(faction);
            return Task.CompletedTask;
        }
    }

}
