using DDDCommerceBCC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DDDCommerceBCC.infra.Interface
{
    public interface IClienteRepository
    {
        Task<Cliente> ObterPorIdAsync(Guid clienteId);
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task RemoverAsync(Guid clienteId);
    }
}
