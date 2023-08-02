using System;
using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "sample-topic-exchange",
                        type: ExchangeType.Topic);

Console.Write("Dinlenecek topic formatini belirtiniz :");

string topic = Console.ReadLine();

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName,
                  exchange:"sample-topic-exchange",
                  routingKey:topic);

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