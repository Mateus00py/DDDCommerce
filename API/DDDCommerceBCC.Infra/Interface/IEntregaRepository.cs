using DDDCommerceBCC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDCommerceBCC.infra.Interface
{
    public interface IEntregaRepository
    {
        Task<Entrega> ObterPorIdAsync(Guid entregaId);
        Task<IEnumerable<Entrega>> ObterTodasAsync();
        Task AdicionarAsync(Entrega entrega);
        Task AtualizarAsync(Entrega entrega);
        Task RemoverAsync(Guid entregaId);
    }
}
