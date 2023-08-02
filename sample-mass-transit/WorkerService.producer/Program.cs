using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkerService.producer.Services;

using IHost host = Host.CreateDefaultBuilder(args)
                  .ConfigureServices(services=>
                  {
                      services.AddMassTransit(configurator =>
                      {
                          configurator.UsingRabbitMq((context, _configurator) =>
                          {
                              _configurator.Host("amqps://localhost:5672");
                          });
                      });

                      services.AddHostedService<PublishMessageService>(provider =>
                      {
                          using IServiceScope scope = provider.CreateScope();

                          IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();

                          return new(publishEndpoint);
                      });
                  })
                    .Build();

host.Run();