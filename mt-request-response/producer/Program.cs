using MassTransit;
using shared.RequestResponseMessage;

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(host: "amqps://localhost:5672");
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri("amqps://localhost:5672/request-queue"));

int i = 1;

while(true)
{
    await Task.Delay(200);
    var response = await request.GetResponse<ResponseMessage>(new RequestMessage() {MessageNo=i,Text = $"{i++}. request"});

    Console.WriteLine($"Response Recieved : {response.Message.Text}");
}


