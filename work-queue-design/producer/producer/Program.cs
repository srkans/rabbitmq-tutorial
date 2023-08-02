using System;
using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

string queueName = "sample-work-queue";

channel.QueueDeclare(queue:queueName,
                     durable:false,
                     exclusive:false,
                     autoDelete:false);

for(int  i = 0; i < 50; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes($"Selam {i}");

    channel.BasicPublish(exchange:string.Empty,
                         routingKey:queueName,
                         body:message);
}

Console.Read();