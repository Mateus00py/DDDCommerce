using DDDCommerceBCC.Domain;
using DDDCommerceBCC.infra;
using DDDCommerceBCC.infra.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace DDDCommerceBCC.Infra.Repositories
{
    public class EntregaRepository : IEntregaRepository
    {
        private readonly AppDbContext _context;

        public EntregaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Entrega> ObterPorIdAsync(Guid entregaId)
        {
            return await _context.Entregas
                .Include(e => e.Pedido)
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(e => e.EntregaId == entregaId);
        }

        public async Task<IEnumerable<Entrega>> ObterTodasAsync()
        {
            return await _context.Entregas
                .Include(e => e.Pedido)
                .Include(e => e.Cliente)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Entrega entrega)
        {
            await _context.Entregas.AddAsync(entrega);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Entrega entrega)
        {
            _context.Entregas.Update(entrega);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid entregaId)
        {
            var entrega = await ObterPorIdAsync(entregaId);
            if (entrega != null)
            {
                _context.Entregas.Remove(entrega);
                await _context.SaveChangesAsync();
            }
        }
    }
}
