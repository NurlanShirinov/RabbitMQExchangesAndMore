using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

string rabbitMQUri = "amqps://mukxipkr:gYUrkZlYfZpCMrPRfKW1DXKsZ7JOG_20@shark.rmq.cloudamqp.com/mukxipkr";

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

ISendEndpoint sendEndPoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.Write("Genderilecek Mesaj : ");
string message = Console.ReadLine();
await sendEndPoint.Send<IMessage>(new ExampleMessage() { Text = message });

Console.Read();