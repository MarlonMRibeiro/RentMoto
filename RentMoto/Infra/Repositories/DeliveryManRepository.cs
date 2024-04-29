using Domain.Entities;
using Domain.Repositories;
using Infra.Context;
using Infra.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class DeliveryManRepository : BaseRepository<DeliveryMan>, IDeliveryManRepository
    {
        public DeliveryManRepository(RentMotoContext dbContext) : base(dbContext) { }

        public DeliveryMan GetByCnh(string cnh)
        {
            cnh = Regex.Replace(cnh, @"\D", "");
            return _context.DeliveryMen.FirstOrDefault(f => f.CnhNumber == cnh && !f.DeleteAt.HasValue);
        }

        public DeliveryMan GetByCnpj(string cnpj)
        {
            cnpj = Regex.Replace(cnpj, @"\D", "");
            return _context.DeliveryMen.FirstOrDefault(f => f.Cnpj == cnpj && !f.DeleteAt.HasValue);
        }

        public DeliveryMan GetByUserId(Guid Id)
        {
            return _context.DeliveryMen.FirstOrDefault(f => f.UserId == Id && !f.DeleteAt.HasValue);
        }

        public IEnumerable<DeliveryMan> GetAllDeliveryManAvaliable()
        {
            var deliveryManRented = _context.Rental.Where(w => !w.EndDate.HasValue).Select(s => s.DeliveryManId);

            var deliveryMans = _context.DeliveryMen.Where(w => deliveryManRented.Contains(w.Id) && !w.DeleteAt.HasValue);

            return deliveryMans;

        }
    }
}
