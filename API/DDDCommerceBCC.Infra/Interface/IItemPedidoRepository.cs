using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDCommerceBCC.Domain;

namespace DDDCommerceBCC.infra.Interface
{
    public interface IItemPedidoRepository
    {
        Task<ItemPedido> ObterPorIdAsync(Guid itemPedidoId);
        Task<IEnumerable<ItemPedido>> ObterTodosAsync();
        Task AdicionarAsync(ItemPedido itemPedido);
        Task AtualizarAsync(ItemPedido itemPedido);
        Task RemoverAsync(Guid itemPedidoId);
    }
}
