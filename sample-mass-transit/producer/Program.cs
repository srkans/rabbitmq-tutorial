using MassTransit;
using shared.Messages;

string queueName = "sample-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(host: "amqps://localhost:5672");
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"amqps://localhost:15672/{queueName}"));

Console.Write("Gonderilecek mesaj : ");
string message = Console.ReadLine();

await sendEndpoint.Send<IMessage>(new SampleMessage()
{
    Text = message
});

Console.Read();

