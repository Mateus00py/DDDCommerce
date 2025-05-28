using DDDCommerceBCC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDCommerceBCC.infra.Interface
{
    public interface IPedidoRepository
    {
        Task<Pedido> ObterPorIdAsync(Guid pedidoId);
        Task<IEnumerable<Pedido>> ObterTodosAsync();
        Task AdicionarAsync(Pedido pedido);
        Task AtualizarAsync(Pedido pedido);
        Task RemoverAsync(Guid pedidoId);
    }
}
