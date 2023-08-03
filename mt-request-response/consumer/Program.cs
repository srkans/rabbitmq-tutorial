using consumer.Consumers;
using MassTransit;

string queueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(host: "amqps://localhost:5672");

    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();

