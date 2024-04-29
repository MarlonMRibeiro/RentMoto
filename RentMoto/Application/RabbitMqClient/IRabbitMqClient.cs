using Application.Dtos;
using Domain.Entities;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RabbitMqClient
{
    public interface IRabbitMqClient
    {
        IModel ConnectChannel();
    }
}
