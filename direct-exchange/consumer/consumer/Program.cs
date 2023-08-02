using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "sample-direct-exchange", type: ExchangeType.Direct);

string queueName = channel.QueueDeclare(exclusive:false).QueueName;

channel.QueueBind(queue:queueName, 
                 exchange:"sample-direct-exchange",
                 routingKey:"sample-direct-queue");

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(queue: queueName, autoAck: true, consumer:consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine($"Consume edildi : {message}");
};

Console.Read();