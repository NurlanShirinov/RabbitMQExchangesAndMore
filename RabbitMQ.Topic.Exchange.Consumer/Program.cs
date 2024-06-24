using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://mukxipkr:gYUrkZlYfZpCMrPRfKW1DXKsZ7JOG_20@shark.rmq.cloudamqp.com/mukxipkr");

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "topic-exchange-example",
    type: ExchangeType.Topic
    );

Console.WriteLine("Dinlenecek topic formatini belirtiniz ");
string topic = Console.ReadLine();

string queuename = channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queuename,
    exchange: "topic-exchange-example",
    routingKey: topic
    );

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(
    queue: queuename,
    autoAck: true,
    consumer: consumer
    );

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();