using MassTransit;
using shared.Messages;

namespace consumer.Consumers
{
    public class SampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen mesaj : {context.Message.Text}");

            return Task.CompletedTask;
        }
    }
}
