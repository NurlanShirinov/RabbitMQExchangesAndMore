using MassTransit;
using RabbitMQ.ESB.MassTransit.WorkerService.Consumer.Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<ExampleMessageConsumer>();
            configurator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps://mukxipkr:gYUrkZlYfZpCMrPRfKW1DXKsZ7JOG_20@shark.rmq.cloudamqp.com/mukxipkr");
                _configurator.ReceiveEndpoint("example-message-queue", e => e.ConfigureConsumer<ExampleMessageConsumer>(context));
            });
        });

    })
    .Build();

await host.RunAsync();