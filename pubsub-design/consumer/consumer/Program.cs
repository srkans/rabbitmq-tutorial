using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

string exchangeName = "sample-pubsub-example";

channel.ExchangeDeclare(exchange: exchangeName,
                        type: ExchangeType.Fanout);

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue:queueName,
                  exchange:exchangeName,
                  routingKey:string.Empty);

channel.BasicQos(prefetchCount:1,
                 prefetchSize:0,
                 global:false);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(queue:queueName,
                     autoAck:false,
                     consumer:consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.ToArray());

    Console.WriteLine($"Message : {message}");
};

Console.Read();