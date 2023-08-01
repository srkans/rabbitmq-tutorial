using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory{HostName="localhost"};

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(queue:"letterbox",durable:false,exclusive:false,autoDelete:false,arguments:null);

var consumer = new EventingBasicConsumer(channel);

