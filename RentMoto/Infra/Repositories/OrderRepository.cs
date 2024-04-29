using Domain.Entities;
using Domain.Repositories;
using Domain.Repositories.Common;
using Infra.Context;
using Infra.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(RentMotoContext dbContext) : base(dbContext) { }

    }
}
