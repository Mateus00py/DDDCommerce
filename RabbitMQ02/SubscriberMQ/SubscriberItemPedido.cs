//using RabbitMQ.Client.Events;
//using RabbitMQ.Client;
//using Models;
//using System.Text;
//using System.Text.Json;
//
//namespace SubscriberMQ
//{
//    public class SubscriberItemPedido
//    {
//        static void Main(string[] args)
//        {
//            var factory = new ConnectionFactory
//            {
//                Uri = new Uri("amqps://zpmcutpo:KEP0T2gAWvLLZKEbGQY1_HXOOVhSvtVo@jackal.rmq.cloudamqp.com/zpmcutpo")
//            };
//
//            using var connection = factory.CreateConnection();
//            using var channel = connection.CreateModel();
//
//            channel.ExchangeDeclare("direct_exchange", ExchangeType.Direct);
//
//            var queueName = "fila_itempedido";
//            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
//            channel.QueueBind(queue: queueName, exchange: "direct_exchange", routingKey: "itempedido");
//
//
//            var consumer = new EventingBasicConsumer(channel);
//            consumer.Received += async (model, ea) =>
//            {
//                var body = ea.Body.ToArray();
//                var json = Encoding.UTF8.GetString(body);
//                var itempedido = JsonSerializer.Deserialize<ItemPedido>(json);
//
//                if (itempedido != null)
//                {
//                    Console.WriteLine($"📨 Item recebido: {itempedido.Nome}");
//
//                    var apiClient = new ApiClient("https://localhost:7240/api/itempedido");
//                    await apiClient.SendAsync(itempedido);
//                }
//                else
//                {
//                    Console.WriteLine("⚠️ Erro ao deserializar o Item.");
//                }
//            };
//
//            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
//            Console.ReadLine();
//        }
//    }
//}
//