using DDDCommerceBCC.Domain;
using DDDCommerceBCC.infra;
using DDDCommerceBCC.infra.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace DDDCommerceBCC.Infra.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> ObterPorIdAsync(Guid clienteId)
        {
            return await _context.Clientes.FindAsync(clienteId);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid clienteId)
        {
            var cliente = await ObterPorIdAsync(clienteId);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
