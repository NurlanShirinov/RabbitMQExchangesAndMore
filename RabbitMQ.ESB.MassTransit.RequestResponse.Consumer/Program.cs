using MassTransit;
using RabbitMQ.ESB.MassTransit.RequestResponse.Consumer.Consumers;

string rabbitMQUri = "amqps://mukxipkr:gYUrkZlYfZpCMrPRfKW1DXKsZ7JOG_20@shark.rmq.cloudamqp.com/mukxipkr";
string requestQueue = "request-queue";
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
    factory.ReceiveEndpoint(requestQueue , endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });
});

await bus.StartAsync();
Console.Read();