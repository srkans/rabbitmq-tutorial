using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

string queueName = "sample-p2p-queue";

channel.QueueDeclare(queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(consumer: consumer,
                     queue : queueName,
                     autoAck:false);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine($"Message : {message}");
};

Console.Read();