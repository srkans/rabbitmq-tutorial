using System;
using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

string exchangeName = "sample-pubsub-example";

channel.ExchangeDeclare(exchange:exchangeName,
                        type:ExchangeType.Fanout);

byte[] message = Encoding.UTF8.GetBytes("Merhaba");

channel.BasicPublish(exchange: exchangeName,
                    routingKey:string.Empty,
                    body:message);

Console.Read();