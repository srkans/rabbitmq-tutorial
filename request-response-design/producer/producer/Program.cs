using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

string requestQueueName = "sample-request-queue";

channel.QueueDeclare(queue:requestQueueName,
                     durable:false,
                     exclusive:false,
                     autoDelete:false);

string responseQueueName = channel.QueueDeclare().QueueName;

string correlationId = Guid.NewGuid().ToString();

//request message
IBasicProperties basicProperties = channel.CreateBasicProperties();

basicProperties.CorrelationId = correlationId;

basicProperties.ReplyTo = responseQueueName;

for(int i = 0; i < 15; i++)
{
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");

    channel.BasicPublish(exchange:string.Empty,
                         routingKey:requestQueueName,
                         body:message,
                         basicProperties:basicProperties);
}

//response message
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(queue:responseQueueName,
                     autoAck:true,
                     consumer:consumer);

consumer.Received += (sender, e) =>
{
    if(e.BasicProperties.CorrelationId == correlationId)
    {
        string message = Encoding.UTF8.GetString(e.Body.Span);

        Console.WriteLine($"Rsponse : {message}");
    }
};

Console.Read();