using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace TraderDirect.Infrastructure.Providers;
public interface IRabbitMqProvider : IDisposable
{
    void Publish(string message);
}

public class RabbitMqProvider : IRabbitMqProvider
{
    private IModel _channel { get; }
    private readonly IConnection _connection;
    private readonly RabbitMqConfig _config;

    public RabbitMqProvider(IOptions<RabbitMqConfig> rabbitMqConfig)
    {
        _config = rabbitMqConfig.Value;
        ConnectionFactory factory = new() { Uri = new Uri(_config.ConnectionString) };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _config.QueueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public void Publish(string message)
    {
        byte[] body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "",
                             routingKey: _config.QueueName,
                             basicProperties: null,
                             body: body);
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}
