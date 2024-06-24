using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

namespace RabbitMQ.ESB.MassTransit.WorkerService.Consumer.Consumer;

public class ExampleMessageConsumer : IConsumer<IMessage>
{
    public Task Consume(ConsumeContext<IMessage> context)
    {
       Console.WriteLine($"Gelen Message : {context.Message.Text}");
        return Task.CompletedTask;
    }
}