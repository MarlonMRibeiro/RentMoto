using Domain.Entities;
using Domain.Repositories;
using Infra.Context;
using Infra.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class MotorcycleRepository : BaseRepository<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(RentMotoContext dbContext) : base(dbContext) { }


        public Motorcycle? GetByPlate(string plate)
        {
            plate = Regex.Replace(plate, @"[^a-zA-Z0-9]", "").ToUpper();
            return _context.Motorcycle.FirstOrDefault(f => f.LicensePlate == plate && !f.DeleteAt.HasValue);
        }

        public Motorcycle? GetAvaliableMotorcycle()
        {
            return _context.Motorcycle.FirstOrDefault(f => f.Status == Domain.Enums.StatusMotorcycle.Available && !f.DeleteAt.HasValue);
        }
    }
}
