using Domain.Base;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Code { get; private set; }
        public decimal Price { get; private set; }
        public StatusOrder Status { get; private set; }

        [ForeignKey("DeliveryMan")]
        public Guid? DeliveryManId { get; private set; }
        public virtual DeliveryMan DeliveryMan { get; set; }

        public static Order Create()
        {
            var order = new Order();

            return order;
        }

        public Order SetCode(string code)
        {
            Code = code;

            return this;
        }

        public Order SetPrice(decimal price)
        {
            Price = price;

            return this;
        }

        public Order SetStatus(StatusOrder status)
        {
            Status = status; 

            return this;
        }

        public Order SetDeliveryManId(Guid deliveryManId)
        {
            DeliveryManId = deliveryManId;

            return this;
        }

        public Order SetAvaliable()
        {
            Status = StatusOrder.Available;

            return this;
        }

        public Order SetAccept()
        {
            Status = StatusOrder.Accepted;

            return this;
        }

        public Order SetDelivered()
        {
            Status = StatusOrder.Delivered;

            return this;
        }

    }
}
