using System;
using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "sample-header-exchange",
                        type: ExchangeType.Headers);

Console.Write("Header value'yu giriniz : ");
string value = Console.ReadLine();

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName,
                  exchange: "sample-header-exchange",
                  routingKey: string.Empty,
                  new Dictionary<string, object>
                  {
                      ["no"] = value
                  });

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(queue: queueName,
                    autoAck:true,
                    consumer:consumer
                    );

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();