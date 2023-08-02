using System;
using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "sample-header-exchange",
                        type:ExchangeType.Headers);

for(int i = 0; i < 10; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba : {i}");

    Console.Write("Header value giriniz : ");
    string value = Console.ReadLine();

    IBasicProperties basicProperties = channel.CreateBasicProperties();
    basicProperties.Headers = new Dictionary<string, object>
    {
        ["no"] = value
    };

    channel.BasicPublish(exchange: "sample-header-exchange",
                        routingKey:string.Empty,
                        body:message,
                        basicProperties:basicProperties);
}

Console.Read();