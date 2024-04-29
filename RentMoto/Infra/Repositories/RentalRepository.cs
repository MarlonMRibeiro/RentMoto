using Domain.Entities;
using Domain.Repositories;
using Infra.Context;
using Infra.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
        public RentalRepository(RentMotoContext dbContext) : base(dbContext) { }


        public Rental GetInProgessByMotorcylceId(Guid motorcycleId)
        {
            return _context.Rental.FirstOrDefault(f => f.MotorcycleId == motorcycleId && !f.EndDate.HasValue);
        }

    }
}
