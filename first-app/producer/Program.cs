using System;
using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory{HostName="localhost"};

//IDisposeble arayuzunu implement ettikleri icin using ile kullandik.
using IConnection connection = factory.CreateConnection();

//islem bittikten sonra nesneyi dispose edip allocation'lari temizlemis oluyoruz.
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue:"letterbox",durable:false,exclusive:false,autoDelete:false,arguments:null);

IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = false;

ConsoleKey? key = null;

do
{
    var message = Console.ReadLine();

    var encodedMessage = Encoding.UTF8.GetBytes(message);

    //we always have to publish to an exchange
    channel.BasicPublish(exchange: "", routingKey: "letterbox", basicProperties:properties, encodedMessage);

    Console.WriteLine($"Published message : {message}");

    key = Console.ReadKey().Key;

} while (key != ConsoleKey.Enter);


