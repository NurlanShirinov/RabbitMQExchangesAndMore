using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://mukxipkr:gYUrkZlYfZpCMrPRfKW1DXKsZ7JOG_20@shark.rmq.cloudamqp.com/mukxipkr");

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

#region P2P (Point-to-Point) Design
//string queueName = "example-p2p-queue";

//channel.QueueDeclare(
//    queue:queueName,
//    durable:false,
//    exclusive:false,
//    autoDelete:false
//    );

//byte[] message = Encoding.UTF8.GetBytes("Merhaba");

//channel.BasicPublish(
//    exchange:string.Empty,
//    routingKey:queueName,
//    body:message
//    );
#endregion
#region Publish/Subscriber (Pub/Sub) design
//string exchangeName = "example-sub-pub-exchange";
//channel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout);

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(1000);

//    byte[] message = Encoding.UTF8.GetBytes("merhaba" + i);

//    channel.BasicPublish(
//        exchange: exchangeName,
//        routingKey: string.Empty,
//        body: message);
//}
#endregion
#region Work Queue design
//string queueName = "exmaple-work-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(1000);

//    byte[] message = Encoding.UTF8.GetBytes("merhaba" + i);

//    channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: queueName,
//        body: message);
//}
#endregion

#region Request/Response design
string requestQueueName = "example-request-response-queue";

channel.QueueDeclare(
    queue: requestQueueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

string replyQueueName = channel.QueueDeclare().QueueName;

string correlationId = Guid.NewGuid().ToString();

#region Request Mesajini yaratmaq vew gondermek
IBasicProperties properties = channel.CreateBasicProperties();
properties.CorrelationId = correlationId;
properties.ReplyTo = replyQueueName;

for (int i = 0; i < 10; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("merhaba" + i);
    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: requestQueueName,
        body: message,
        basicProperties: properties);
};
#endregion

#region Response Kuyrugu Dinleme
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: replyQueueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    if (e.BasicProperties.CorrelationId == correlationId)
    {
        Console.WriteLine($"Response : {Encoding.UTF8.GetString(e.Body.Span)}");
    }
};
#endregion

#endregion

Console.Read();