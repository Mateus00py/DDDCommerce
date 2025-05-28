using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDCommerceBCC.Domain
{
    public class Entrega
    {
        public Guid EntregaId { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Pedido Pedido { get; set;}
    }
}
