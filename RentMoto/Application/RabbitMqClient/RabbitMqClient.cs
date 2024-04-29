using Application.Dtos;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.RabbitMqClient
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly IConfiguration _configuration;

        public RabbitMqClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IModel ConnectChannel()
        {
            var connection = new ConnectionFactory()
            {
                HostName = _configuration["rabbitMQHost"],
                Port = 5672
            }.CreateConnection();

            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "orderNotification", durable: false, exclusive: false, autoDelete: false);

            return channel;
        }
    }
}
