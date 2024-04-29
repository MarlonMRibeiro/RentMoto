using Domain.Entities;
using Domain.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IMotorcycleRepository : IBaseRepository<Motorcycle>
    {
        public Motorcycle? GetByPlate(string plate);
        Motorcycle? GetAvaliableMotorcycle();
    }
}
