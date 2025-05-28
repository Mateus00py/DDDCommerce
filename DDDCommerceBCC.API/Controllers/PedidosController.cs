using DDDCommerceBCC.Domain;
using DDDCommerceBCC.infra.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DDDCommerceBCC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var pedidos = await _pedidoRepository.ObterTodosAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(Guid id)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            await _pedidoRepository.AdicionarAsync(pedido);
            return CreatedAtAction(nameof(GetPorId), new { id = pedido.PedidoId }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Pedido pedido)
        {
            if (id != pedido.PedidoId) return BadRequest();
            await _pedidoRepository.AtualizarAsync(pedido);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _pedidoRepository.RemoverAsync(id);
            return NoContent();
        }
    }
}
