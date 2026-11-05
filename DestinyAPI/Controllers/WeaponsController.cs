using DestinyAPI.Models;
using DestinyAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DestinyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponsController : ControllerBase
    {
        private readonly IWeaponRepository _repository;

        public WeaponsController(IWeaponRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weapon>>> GetAll()
        {
            var weapons = await _repository.GetAllAsync();
            return Ok(weapons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Weapon>> GetById(int id)
        {
            var weapon = await _repository.GetByIdAsync(id);
            return weapon is null ? NotFound() : Ok(weapon);
        }

        [HttpPost]
        public async Task<ActionResult<Weapon>> Create(Weapon newWeapon)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _repository.CreateAsync(newWeapon);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Weapon weapon)
        {
            if (id != weapon.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _repository.UpdateAsync(weapon);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
