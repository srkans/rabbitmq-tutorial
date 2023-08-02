using MassTransit;
using Microsoft.Extensions.Hosting;
using shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.producer.Services
{
    public class PublishMessageService : BackgroundService
    {
        private readonly IPublishEndpoint _publishEndPoint;

        public PublishMessageService(IPublishEndpoint publishEndpoint)
        {
            _publishEndPoint = publishEndpoint;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;

            while (true)
            {
                SampleMessage message = new SampleMessage()
                {
                    Text = $"{++i} mesaj"
                };

                await _publishEndPoint.Publish(message);
            }
        }
    }
}
