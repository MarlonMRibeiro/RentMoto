using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class OrderNotificationDto
    {
        public Guid OrderId { get; set; }
        public Guid DeliveryManId { get; set; }
        public string Message { get; set; }
    }
}
