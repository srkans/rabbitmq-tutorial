using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory{HostName="localhost"};

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

//consumer tarafindaki kuyruk publisher ile bire bir ayni yapida olmalidir.
channel.QueueDeclare(queue:"letterbox",durable:false,exclusive:false,autoDelete:false,arguments:null);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

//kuyruga gelen mesaji proses ediyoruz
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"message received : {message}");
};

channel.BasicConsume(queue:"letterbox", autoAck:true, consumer: consumer);

Console.ReadKey();