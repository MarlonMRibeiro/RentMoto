using Domain.Entities;
using Domain.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IDeliveryManRepository : IBaseRepository<DeliveryMan>
    {
        DeliveryMan GetByCnh(string cnh);
        DeliveryMan GetByCnpj(string cnpj);
        DeliveryMan GetByUserId(Guid Id);
        IEnumerable<DeliveryMan> GetAllDeliveryManAvaliable();
    }
}
