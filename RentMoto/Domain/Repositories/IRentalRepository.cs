using Domain.Entities;
using Domain.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRentalRepository : IBaseRepository<Rental>
    {
        Rental GetInProgessByMotorcylceId(Guid motorcycleId);
    }
}
