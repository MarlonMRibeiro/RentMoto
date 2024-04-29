using Domain.Base;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Motorcycle : BaseEntity
    {
        public string Model { get; private set; }
        public string Identity { get; private set; }
        public string LicensePlate { get; private set; }
        public string Year { get; private set; }
        public StatusMotorcycle Status { get; private set; }

        public static Motorcycle Create()
        {
            var motorCycle = new Motorcycle();

            return motorCycle;
        }

        public Motorcycle SetModel(string model)
        {
            Model = model;

            return this;
        }

        public Motorcycle SetIdentity(string identity)
        {
            Identity = identity;

            return this;
        }

        public Motorcycle SetLicensePlate(string licensePlate)
        {
            LicensePlate = Regex.Replace(licensePlate, @"[^a-zA-Z0-9]", "").ToUpper();

            return this;
        }

        public Motorcycle SetYear(string year)
        {
            Year = Regex.Replace(year, @"\D", "");

            return this;
        }

        public Motorcycle Rent()
        {
            Status = StatusMotorcycle.Rented; 
            
            return this;
        }

        public Motorcycle ToMakeAvailable()
        {
            Status = StatusMotorcycle.Available; 
            
            return this;
        }
    }
}
