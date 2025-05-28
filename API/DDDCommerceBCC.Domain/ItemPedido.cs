using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDCommerceBCC.Domain
{
    public class ItemPedido
    {
        public Guid ItemPedidoId { get; set; }
        public string Nome { get; set;}
        public string Categoria { get; set;}
        public decimal Preco{ get; set;}
    }
}
