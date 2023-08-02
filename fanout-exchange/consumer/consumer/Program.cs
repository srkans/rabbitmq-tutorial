using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "sample-fanout-exchange", type: ExchangeType.Fanout);

Console.Write("Enter queue name : ");

string queueName = Console.ReadLine();

channel.QueueDeclare(queue:queueName,exclusive:false);

channel.QueueBind(queue: queueName, 
                  exchange: "sample-fanout-exchange",
                  routingKey:string.Empty);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(queue: queueName,
                     autoAck:true,
                     consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);

    Console.WriteLine($"Mesaj : {message}");
};

Console.Read();