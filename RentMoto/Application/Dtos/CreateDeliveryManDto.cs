using Domain.Enums;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateDeliveryManDto
    {
        public string Identity { get; set; }
        public string Cnpj { get; set; }
        public DateTime Birth { get; set; }
        public string CnhNumber { get; set; }
        public TypeCnh CnhType { get; set; }
    }
}
