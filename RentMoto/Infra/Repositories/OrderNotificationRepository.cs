using Domain.Entities;
using Domain.Repositories;
using Infra.Context;
using Infra.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class OrderNotificationRepository : BaseRepository<OrderNotification>, IOrderNotificationRepository
    {
        public OrderNotificationRepository(RentMotoContext dbContext) : base(dbContext) { }

        public OrderNotification GetByDeliveryManIdAndOrderId(Guid deliveryManId, Guid orderId)
        {
            return _context.OrderNotification.FirstOrDefault(f => f.DeliveryManId == deliveryManId && f.OrderId == orderId && !f.DeleteAt.HasValue);
        }
    }
}
