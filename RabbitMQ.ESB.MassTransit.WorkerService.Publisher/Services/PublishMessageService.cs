
using MassTransit;
using MassTransit.Testing;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

namespace RabbitMQ.ESB.MassTransit.WorkerService.Publisher.Services;

public class PublishMessageService : BackgroundService
{
    readonly IPublishEndpoint _publishEndpoint;

    public PublishMessageService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int i = 0;
        while (true)
        {
            ExampleMessage message = new()
            {
                Text = $"{++i}. message"
            };
            await _publishEndpoint.Publish(message);
        }
    }
}