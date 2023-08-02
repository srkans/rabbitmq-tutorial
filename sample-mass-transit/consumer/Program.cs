using consumer.Consumers;
using MassTransit;

string queueName = "sample-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(host: "amqps://localhost:5672");

    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<SampleMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();

