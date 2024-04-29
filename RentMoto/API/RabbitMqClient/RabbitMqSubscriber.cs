using Application.RabbitMqClient;
using Application.UseCases.Interface;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RabbitMqClient
{
    public class RabbitMqSubscriber : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;

        public RabbitMqSubscriber(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var channel = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IRabbitMqClient>().ConnectChannel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (moduleHandle, ea) =>
            {
                ReadOnlyMemory<byte> body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IOrderNotificationUseCase>().ReceiveNotification(message);
            };

            channel.BasicConsume(queue: "orderNotification", autoAck: true, consumer: consumer);

            return Task.Delay(Timeout.Infinite);
        }
    }
}
