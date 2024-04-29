using Application.Dtos;
using Application.UseCases.Interface;
using Application.ViewModel;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class OrderNotificationUseCase : IOrderNotificationUseCase
    {
        private readonly IOrderNotificationRepository _orderNotificationRepository;
        public OrderNotificationUseCase(IOrderNotificationRepository orderNotificationRepository)
        {
            _orderNotificationRepository = orderNotificationRepository;
        }
        public void ReceiveNotification(string message)
        {
            var data = JsonSerializer.Deserialize<OrderNotificationDto>(message);

            var orderNotification = OrderNotification.Create();

            orderNotification
            .SetOrderId(data.OrderId)
            .SetDeliveryManId(data.DeliveryManId)
            .SetMessage(data.Message);

            _orderNotificationRepository.Add(orderNotification);
        }

        public IEnumerable<OrderNotification> GetAll()
        {
            return _orderNotificationRepository.GetAll().OrderByDescending(o => o.CreatedAt);
        }
    }
}
