namespace RabbitMQ.ESB.MassTransit.Shared.RequestResponseMessages;

public record RequestMessage
{
    public int MessageNo { get; set; }
    public string Text { get; set; }
}