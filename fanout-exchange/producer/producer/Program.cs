using System;
using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "sample-fanout-exchange", type: ExchangeType.Fanout);

for(int i = 0; i < 100; i++)
{
    await Task.Delay(100);

    byte[] message = Encoding.UTF8.GetBytes($"Merhaba : {i}");

    channel.BasicPublish(exchange: "sample-fanout-exchange",
                         routingKey: string.Empty,
                         body:message);
}

Console.Read();