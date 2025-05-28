using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDCommerceBCC.Domain
{
    public class Pedido
    {
        public Guid PedidoId { get; set; }
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DeliveryDate { get; set; }

        public void AdicionarItem(ItemPedido item)
        {
            Itens.Add(item);
        }

        public void RemoverItem(Guid itemId)
        {
            var item = Itens.FirstOrDefault(i => i.ItemPedidoId == itemId);
            if (item != null)
            {
                Itens.Remove(item);
            }
        }
    }
}
