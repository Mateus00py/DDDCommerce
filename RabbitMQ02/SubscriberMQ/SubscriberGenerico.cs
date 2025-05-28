using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Models;
using System.Text;
using System.Text.Json;

namespace SubscriberMQ
{
    public class SubscriberGenerico
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://zpmcutpo:KEP0T2gAWvLLZKEbGQY1_HXOOVhSvtVo@jackal.rmq.cloudamqp.com/zpmcutpo")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            string exchangeName = "direct_exchange";
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

            string queueName = "fila_geral";
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Roteamentos que queremos escutar
            string[] routingKeys = { "cliente", "itempedido" };
            foreach (var routingKey in routingKeys)
            {
                channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);
            }

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var routingKey = ea.RoutingKey;
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                Console.WriteLine($"📩 Mensagem recebida com routingKey '{routingKey}': {json}");


                try
                {
                    switch (routingKey)
                    {
                        case "cliente":
                            var cliente = JsonSerializer.Deserialize<Cliente>(json);
                            var apiCliente = new ApiClient("https://localhost:7240/api/cliente");
                            if (cliente != null)
                                await apiCliente.SendAsync(cliente);
                            break;

                        case "itempedido":
                            var itempedido = JsonSerializer.Deserialize<ItemPedido>(json);
                            var apiItemPedido = new ApiClient("https://localhost:7240/api/itempedido");
                            if (itempedido != null)
                                await apiItemPedido.SendAsync(itempedido);
                            break;

                        default:
                            Console.WriteLine($"⚠️ RoutingKey desconhecida: {routingKey}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro ao processar mensagem: {ex.Message}");
                }
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            Console.WriteLine("👂 Subscriber genérico rodando. Pressione Enter para sair...");
            Console.ReadLine();
        }
    }
}
