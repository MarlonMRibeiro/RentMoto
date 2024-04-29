using Application.Dtos;
using Application.RabbitMqClient;
using Application.UseCases.Interface;
using Application.ViewModel;
using Domain.Entities;
using Domain.Repositories;
using Nest;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class OrderUseCase : IOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRabbitMqClient _rabbitMqClient;
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IOrderNotificationRepository _orderNotificationRepository;

        public OrderUseCase(IOrderRepository orderRepository, IRabbitMqClient rabbitMqClient, IDeliveryManRepository deliveryManRepository, IOrderNotificationRepository orderNotificationRepository)
        {
            _orderRepository = orderRepository;
            _rabbitMqClient = rabbitMqClient;
            _deliveryManRepository = deliveryManRepository;
            _orderNotificationRepository = orderNotificationRepository;
        }

        public ResponseViewModel CreateOrder(CreateOrderDto newOrder)
        {
            var order = Order.Create();

            order
                .SetPrice(newOrder.Price)
                .SetCode(newOrder.Code)
                .SetAvaliable();

            _orderRepository.Add(order);

            var notifications = _deliveryManRepository.GetAllDeliveryManAvaliable().Select(s => new OrderNotificationDto()
            {
                Message = $"The order {order.Code} is Available!",
                OrderId = order.Id,
                DeliveryManId = s.Id
            }).ToList();


            var channel = _rabbitMqClient.ConnectChannel();

            //channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            foreach (var notificacao in notifications)
            {
                var message = JsonConvert.SerializeObject(notificacao);
                var body = Encoding.UTF8.GetBytes(message);


                channel.BasicPublish(string.Empty,
                    routingKey: "orderNotification",
                    basicProperties: null,
                    body: body
                );
            }

            return new ResponseViewModel(true, "Sucess to create!");
        }

        public ResponseViewModel TakeOrder(Guid orderId, Guid userId)
        {
            var deliveryMan = _deliveryManRepository.GetByUserId(userId);

            if (deliveryMan == null)
                return new ResponseViewModel(false, "Error to take: DeliveryMan not found!");

            var order = _orderRepository.GetById(orderId);

            if (order == null)
                return new ResponseViewModel(false, "Error to take: Order not found!");

            if (order.Status == Domain.Enums.StatusOrder.Accepted || order.Status == Domain.Enums.StatusOrder.Delivered)
                return new ResponseViewModel(false, "Error to take: Order not avaliable!");

            var notification = _orderNotificationRepository.GetByDeliveryManIdAndOrderId(deliveryMan.Id, order.Id);

            if (notification == null)
                return new ResponseViewModel(false, "Error to take: DeliveryMan dont received notification!");

            order
                .SetAccept()
                .SetDeliveryManId(deliveryMan.Id);

            _orderRepository.Update(order);

            return new ResponseViewModel(true, "Sucess to take!");
        }

        public ResponseViewModel FinalizeOrder(Guid orderId, Guid userId)
        {
            var order = _orderRepository.GetById(orderId);

            if (order == null)
                return new ResponseViewModel(false, "Error to deliver: Order not found!");

            var deliveryMan = _deliveryManRepository.GetByUserId(userId);

            if (deliveryMan == null || order.DeliveryManId != deliveryMan.Id)
                return new ResponseViewModel(false, "Error to deliver: Order can only be completed by the responsible delivery person!");

            if (order.Status == Domain.Enums.StatusOrder.Delivered)
                return new ResponseViewModel(false, "Error to deliver: Order already delivered!");

            if (order.Status == Domain.Enums.StatusOrder.Available)
                return new ResponseViewModel(false, "Error to deliver: Order is not ready to be finalized!");

            order.SetDelivered();

            _orderRepository.Update(order);

            return new ResponseViewModel(true, "Sucess to deliver!");
        }
    }
}
