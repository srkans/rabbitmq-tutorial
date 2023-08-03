using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using shared.RequestResponseMessage;

namespace consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Text);

            await context.RespondAsync(new ResponseMessage() {Text = $"{context.Message.MessageNo}. response to request" });
        }
    }
}