using DDDCommerceBCC.Domain;
using DDDCommerceBCC.infra.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DDDCommerceBCC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var clientes = await _clienteRepository.ObterTodosAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cliente cliente)
        {
            await _clienteRepository.AdicionarAsync(cliente);
            return CreatedAtAction(nameof(GetPorId), new { id = cliente.ClienteId }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Cliente cliente)
        {
            if (id != cliente.ClienteId) return BadRequest();
            await _clienteRepository.AtualizarAsync(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _clienteRepository.RemoverAsync(id);
            return NoContent();
        }
    }
}