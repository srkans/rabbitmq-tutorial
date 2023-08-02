using System;
using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "sample-direct-exchange", type: ExchangeType.Direct);

while (true)
{
    Console.Write("Mesaj : ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange:"sample-direct-exchange",
                         routingKey:"sample-direct-queue", 
                         body:byteMessage);
}

Console.Read();