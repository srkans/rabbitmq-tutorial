using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

string requestQueueName = "sample-request-queue";

channel.QueueDeclare(queue: requestQueueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(queue:requestQueueName,
                     autoAck:true,
                     consumer:consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);

    Console.WriteLine(message);

    byte[] responseMessage = Encoding.UTF8.GetBytes($"Islem tamamlandi : {message}");

    IBasicProperties basicProperties = channel.CreateBasicProperties();

    basicProperties.CorrelationId = e.BasicProperties.CorrelationId;

    channel.BasicPublish(exchange:string.Empty,
                         routingKey:e.BasicProperties.ReplyTo,
                         basicProperties:basicProperties,
                         body:responseMessage);

};

Console.Read();