using TradesMessageConsumer;

string rabbitMqConnectionString = "amqp://guest:guest@localhost:5672/";
string queueName = "trades_executed";
RabbitMqConsumer consumer = new(queueName, rabbitMqConnectionString);

Console.WriteLine("Starting RabbitMQ consumer...");
consumer.StartListening();