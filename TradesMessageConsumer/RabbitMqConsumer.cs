using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TradesMessageConsumer;
public class RabbitMqConsumer
{
    private readonly string _queueName;
    private readonly string _rabbitMqConnectionString;

    public RabbitMqConsumer(string queueName, string rabbitMqConnectionString)
    {
        _queueName = queueName;
        _rabbitMqConnectionString = rabbitMqConnectionString;
    }

    public void StartListening()
    {
        ConnectionFactory factory = new() { Uri = new Uri(_rabbitMqConnectionString) };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _queueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"[x] Received trade message: {message}");
        };

        channel.BasicConsume(queue: _queueName,
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine("Listening for trade messages. Press [enter] to exit.");
        Console.ReadLine();
    }
}


