using MassTransit;
using Microsoft.Extensions.Hosting;
using WorkerService.consumer.Consumers;

using IHost host = Host.CreateDefaultBuilder(args)
                  .ConfigureServices(services =>
                  {
                      services.AddMassTransit(configurator =>
                      {
                          configurator.AddConsumer<SampleMessageConsumer>();

                          configurator.UsingRabbitMq((context, _configurator) =>
                          {
                              _configurator.Host("amqps://localhost:5672");

                              _configurator.ReceiveEndpoint("sample-message-queue", e =>                     e.ConfigureConsumer<SampleMessageConsumer>(context));
                              
                          });
                      });
                  })
                    .Build();

host.Run();