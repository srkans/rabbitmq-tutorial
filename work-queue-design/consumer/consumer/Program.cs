using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

string queueName = "sample-work-queue";

channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(queue:queueName,
                     autoAck:true,
                     consumer:consumer);

channel.BasicQos(prefetchCount:1,
                 prefetchSize:0,
                 global:false);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);

    Console.WriteLine(message);
};

Console.Read();