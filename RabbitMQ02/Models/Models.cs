
namespace Models
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
    }
    public class ItemPedido
    {
        public Guid ItemPedidoId { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
    }
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
    public class Entrega
    {
        public Guid EntregaId { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Pedido Pedido { get; set; }
    }
}
