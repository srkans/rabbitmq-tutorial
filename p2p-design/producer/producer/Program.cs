using System;
using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

string queueName = "sample-p2p-queue";

channel.QueueDeclare(queue: queueName,
                    durable:false,
                    exclusive:false,
                    autoDelete:false);

byte[] message = Encoding.UTF8.GetBytes("Merhaba");

channel.BasicPublish(exchange: string.Empty,
                     routingKey: queueName,
                     body:message);

Console.Read();