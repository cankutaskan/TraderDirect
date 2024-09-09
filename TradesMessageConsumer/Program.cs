using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Exceptions;
using TradesMessageConsumer;


var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

string? cs = config.GetSection("RabbitMq:ConnectionString").Value;
string? queueName = config.GetSection("RabbitMq:QueueName").Value;

Console.WriteLine($"ConnectionString:{cs}");


if (string.IsNullOrEmpty(cs) || string.IsNullOrEmpty(queueName))
    throw new Exception("RabbitMq is not configured correctly");

var retryCount = 0;
const int maxRetries = 10;
const int retryDelay = 5000; 

while (retryCount < maxRetries)
{
    try
    {
        RabbitMqConsumer consumer = new(queueName, cs);
        consumer.StartListening();
        Console.WriteLine("Connected to RabbitMQ");
        break;
    }
    catch (BrokerUnreachableException ex)
    {
        retryCount++;
        Console.WriteLine($"Failed to connect to RabbitMQ. Retry {retryCount}/{maxRetries}. Exception: {ex.Message}");
        Thread.Sleep(retryDelay);
    }
}


Console.WriteLine("Starting RabbitMQ consumer...");
