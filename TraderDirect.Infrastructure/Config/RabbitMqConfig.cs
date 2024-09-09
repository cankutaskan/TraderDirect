public record RabbitMqConfig
{
    public required string ConnectionString { get; set; }
    public required string QueueName { get; set; }
}

