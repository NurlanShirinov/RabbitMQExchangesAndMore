//Gedishat
//1. Publisherde olan exhange ile bire bir eynisin consumerdede tanimlayiriq.
//2. Publisher terefinden routing keyde olan deyerdeki queueu -ye gonderilen mesajlari oz declare etdiyim queue yonlendirmeyimiz lazimdir.
//bunun ucun ilk olaraq bir queue yaratmaliyiq
//


using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://mukxipkr:gYUrkZlYfZpCMrPRfKW1DXKsZ7JOG_20@shark.rmq.cloudamqp.com/mukxipkr");

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

//1
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

//2
string queueName = channel.QueueDeclare().QueueName;


//3
channel.QueueBind(
    queue:queueName , 
    exchange: "direct-exchange-example" , 
    routingKey: "direct-queue-example");


EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue : queueName, autoAck:true , consumer: consumer);

consumer.Received += (sender , e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();