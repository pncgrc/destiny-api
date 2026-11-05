using DestinyAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyAPI.Repositories
{
    public class InMemoryCharacterRepository : ICharacterRepository
    {
        private readonly List<Character> _characters = new()
    {
        new Character { Id = 1, Name = "Ikora Rey", WeaponId = 1 },
        new Character { Id = 2, Name = "Savathûn", WeaponId = 2 },
        new Character { Id = 3, Name = "Taniks", WeaponId = 3 }
    };

        public Task<IEnumerable<Character>> GetAllAsync()
        {
            return Task.FromResult(_characters.AsEnumerable());
        }

        public Task<Character?> GetByIdAsync(int id)
        {
            var character = _characters.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(character);
        }

        public Task<Character> AddAsync(Character character)
        {
            character.Id = _characters.Max(c => c.Id) + 1;
            _characters.Add(character);
            return Task.FromResult(character);
        }

        public Task UpdateAsync(Character character)
        {
            var index = _characters.FindIndex(c => c.Id == character.Id);
            if (index != -1)
            {
                _characters[index] = character;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var character = _characters.FirstOrDefault(c => c.Id == id);
            if (character != null)
            {
                _characters.Remove(character);
            }
            return Task.CompletedTask;
        }
    }
}
