using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Models;

namespace PublisherMQ
{
    public class RabbitPublisher
    {
        private readonly IModel _channel;
        private readonly string _exchangeName;

        public RabbitPublisher(IModel channel, string exchangeName)
        {
            _channel = channel;
            _exchangeName = exchangeName;

            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);
        }

        public void Publish<T>(string routingKey, T data)
        {
            string json = JsonSerializer.Serialize(data);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(
                exchange: _exchangeName,
                routingKey: routingKey,
                basicProperties: null,
                body: body
            );

            Console.WriteLine($"📤 Enviado com routingKey '{routingKey}': {json}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://zpmcutpo:KEP0T2gAWvLLZKEbGQY1_HXOOVhSvtVo@jackal.rmq.cloudamqp.com/zpmcutpo")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var publisher = new RabbitPublisher(channel, "direct_exchange");

            // Enviando um cliente
            var cliente = new 
            {
                Name = "Bruce Alan",
                Email = "Bruce@gmail.com",
                Endereco = "Zaum",
                Telefone = "39890654"

            };
            publisher.Publish("cliente", cliente);

            // Enviando um item de pedido
            var itempedido = new
            {
                Nome = "Marreta",
                Categoria = "Construção",
                Preco = 25.99
            };
            publisher.Publish("itempedido", itempedido);

            Console.WriteLine("✅ Mensagens enviadas. Pressione Enter para sair...");
            Console.ReadLine();
        }
    }
}
