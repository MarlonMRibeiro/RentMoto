using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderNotification : BaseEntity
    {
        [ForeignKey("Order")]
        public Guid OrderId { get; private set; }
        public virtual Order Order { get; set; }

        [ForeignKey("DeliveryMan")]
        public Guid DeliveryManId { get; private set; }
        public virtual DeliveryMan DeliveryMan { get; set; }

        public string Message { get; private set; }

        public static OrderNotification Create()
        {
            var order = new OrderNotification();

            return order;
        }

        public OrderNotification SetOrderId(Guid orderId)
        {
            OrderId = orderId;

            return this;
        }

        public OrderNotification SetDeliveryManId(Guid deliveryManId)
        {
            DeliveryManId = deliveryManId;

            return this;
        }

        public OrderNotification SetMessage(string message)
        {
            Message = message; 
            
            return this;
        }
    }
}
