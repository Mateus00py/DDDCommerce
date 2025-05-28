using DDDCommerceBCC.Domain;
using DDDCommerceBCC.infra.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DDDCommerceBCC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemPedidoController : ControllerBase
    {
        private readonly IItemPedidoRepository _itemPedidoRepository;

        public ItemPedidoController(IItemPedidoRepository itemPedidoRepository)
        {
            _itemPedidoRepository = itemPedidoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var itens = await _itemPedidoRepository.ObterTodosAsync();
            return Ok(itens);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(Guid id)
        {
            var item = await _itemPedidoRepository.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemPedido item)
        {
            await _itemPedidoRepository.AdicionarAsync(item);
            return CreatedAtAction(nameof(GetPorId), new { id = item.ItemPedidoId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ItemPedido item)
        {
            if (id != item.ItemPedidoId) return BadRequest();
            await _itemPedidoRepository.AtualizarAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _itemPedidoRepository.RemoverAsync(id);
            return NoContent();
        }
    }
}
