using DestinyAPI.Models;
using DestinyAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DestinyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactionsController : ControllerBase
    {
        private readonly IFactionRepository _repository;

        public FactionsController(IFactionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faction>>> GetAll()
        {
            var factions = await _repository.GetAllAsync();
            return Ok(factions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Faction>> GetById(int id)
        {
            var faction = await _repository.GetByIdAsync(id);
            if (faction == null)
                return NotFound();
            return Ok(faction);
        }

        [HttpPost]
        public async Task<ActionResult<Faction>> Create(Faction newFaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdFaction = await _repository.AddAsync(newFaction);
            return CreatedAtAction(nameof(GetById), new { id = createdFaction.Id }, createdFaction);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Faction updatedFaction)
        {
            if (id != updatedFaction.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.UpdateAsync(updatedFaction);
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
