using DestinyAPI.Models;
using DestinyAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DestinyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterRepository _repository;

        public CharactersController(ICharacterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetAll()
        {
            var characters = await _repository.GetAllAsync();
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetById(int id)
        {
            var character = await _repository.GetByIdAsync(id);
            if (character == null) return NotFound();
            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<Character>> Create(Character newCharacter)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdCharacter = await _repository.AddAsync(newCharacter);
            return CreatedAtAction(nameof(GetById), new { id = createdCharacter.Id }, createdCharacter);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Character updatedCharacter)
        {
            if (id != updatedCharacter.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _repository.UpdateAsync(updatedCharacter);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
