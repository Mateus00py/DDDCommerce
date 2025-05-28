using DDDCommerceBCC.Domain;
using DDDCommerceBCC.infra;
using DDDCommerceBCC.infra.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace DDDCommerceBCC.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> ObterPorIdAsync(Guid pedidoId)
        {
            return await _context.Pedidos  
                .Include(p => p.Itens)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
        }

        public async Task<IEnumerable<Pedido>> ObterTodosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .Include(p => p.Cliente)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid pedidoId)
        {
            var pedido = await ObterPorIdAsync(pedidoId);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}
