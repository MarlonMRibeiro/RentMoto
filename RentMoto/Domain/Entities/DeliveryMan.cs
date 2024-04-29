using Domain.Base;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DeliveryMan : BaseEntity
    {
        public string Identity { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime Birth { get; private set; }
        public string CnhNumber { get; private set; }
        public TypeCnh CnhType { get; private set; }
        public string? CnhImage { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; private set; }
        public virtual User User { get; set; }


        public static DeliveryMan Create()
        {
            return new DeliveryMan();
        }

        public DeliveryMan SetUserId(Guid userId)
        {
            UserId = userId;
            
            return this;      
        }


        public DeliveryMan SetIdentity(string identity)
        {
            Identity =  Regex.Replace(identity, @"\D", "");

            return this;
        }

        public DeliveryMan SetCnpj(string cnpj)
        {
            Cnpj = Regex.Replace(cnpj, @"\D", "");

            return this;
        }

        public DeliveryMan SetBirth(DateTime birth)
        {
            Birth = birth;

            return this;
        }

        public DeliveryMan SetCnhNumber(string cnhNumber)
        {

            CnhNumber = Regex.Replace(cnhNumber, @"\D", "");

            return this;
        }

        public DeliveryMan SetCnhType(TypeCnh cnhType)
        {
            CnhType = cnhType;

            return this;
        }

        public DeliveryMan SetCnhImage(string cnhImage)
        {
            CnhImage = cnhImage;

            return this;
        }
    }
}
