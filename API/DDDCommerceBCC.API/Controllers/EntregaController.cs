using DDDCommerceBCC.Domain;
using DDDCommerceBCC.infra.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DDDCommerceBCC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntregaController : ControllerBase
    {
        private readonly IEntregaRepository _entregaRepository;

        public EntregaController(IEntregaRepository entregaRepository)
        {
            _entregaRepository = entregaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodas()
        {
            var entregas = await _entregaRepository.ObterTodasAsync();
            return Ok(entregas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(Guid id)
        {
            var entrega = await _entregaRepository.ObterPorIdAsync(id);
            if (entrega == null) return NotFound();
            return Ok(entrega);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Entrega entrega)
        {
            await _entregaRepository.AdicionarAsync(entrega);
            return CreatedAtAction(nameof(GetPorId), new { id = entrega.EntregaId }, entrega);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Entrega entrega)
        {
            if (id != entrega.EntregaId) return BadRequest();
            await _entregaRepository.AtualizarAsync(entrega);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _entregaRepository.RemoverAsync(id);
            return NoContent();
        }
    }
}
