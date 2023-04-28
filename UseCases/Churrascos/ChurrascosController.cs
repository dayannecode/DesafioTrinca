using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DesafioTrica.Domain;
using DesafioTrica.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioTrica.UseCases.Churrascos
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChurrascosController : ControllerBase
    {
        private readonly IChurrascoRepository _repository;

        public ChurrascosController(IChurrascoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Churrasco>>> GetChurrascosAsync()
        {
            var churrascos = await _repository.GetChurrascosAsync();
            return Ok(churrascos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Churrasco>> GetChurrascoAsync(int id)
        {
            var churrasco = await _repository.GetChurrascoAsync(id);

            if (churrasco == null)
            {
                return NotFound();
            }

            return Ok(churrasco);
        }

        [HttpPost]
        public async Task<ActionResult<Churrasco>> AddChurrascoAsync([FromBody] Churrasco churrasco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _repository.AddChurrascoAsync(churrasco);

            return CreatedAtAction(nameof(GetChurrascoAsync), new { id }, churrasco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChurrascoAsync(int id, [FromBody] Churrasco churrasco)
        {
            if (id != churrasco.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _repository.UpdateChurrascoAsync(churrasco);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ixe, deu zebra no churras, tente novamente mais tarde.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChurrascoAsync(int id)
        {
            var churrasco = await _repository.GetChurrascoAsync(id);
            if (churrasco == null)
            {
                return NotFound();
            }

            var result = await _repository.DeleteChurrascoAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
