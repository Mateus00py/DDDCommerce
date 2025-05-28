using DDDCommerceBCC.Domain;
using DDDCommerceBCC.infra;
using DDDCommerceBCC.infra.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace DDDCommerceBCC.Infra.Repositories
{
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private readonly AppDbContext _context;

        public ItemPedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ItemPedido> ObterPorIdAsync(Guid itemPedidoId)
        {
            return await _context.ItensPedido.FindAsync(itemPedidoId);
        }

        public async Task<IEnumerable<ItemPedido>> ObterTodosAsync()
        {
            return await _context.ItensPedido.ToListAsync();
        }

        public async Task AdicionarAsync(ItemPedido itemPedido)
        {
            await _context.ItensPedido.AddAsync(itemPedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ItemPedido itemPedido)
        {
            _context.ItensPedido.Update(itemPedido);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid itemPedidoId)
        {
            var item = await ObterPorIdAsync(itemPedidoId);
            if (item != null)
            {
                _context.ItensPedido.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
