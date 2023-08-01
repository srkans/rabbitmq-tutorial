﻿using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory{HostName="localhost"};

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(queue:"letterbox",durable:false,exclusive:false,autoDelete:false,arguments:null);

var message = "This is my first message";

var encodedMessage = Encoding.UTF8.GetBytes(message);

//we always have to publish to an exchange
channel.BasicPublish("","letterbox", null, encodedMessage);

Console.WriteLine($"Published message : {message}");