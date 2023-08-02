using System;
using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange:"sample-topic-exchange",
                        type:ExchangeType.Topic);

for(int i = 0; i < 15; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes($"Merhaba : {i}");

    Console.Write("Gonderilecek topic formatini belirtiniz : ");

    string topic = Console.ReadLine();

    channel.BasicPublish(exchange: "sample-topic-exchange",
                         routingKey:topic,
                         body:message);
}

Console.Read();