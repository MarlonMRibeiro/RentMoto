using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Plan : BaseEntity
    {
        public int Days { get; private set; }
        public decimal DailyPrice { get; private set; }
        public decimal PenaltyPercentage { get; private set; }

        public static Plan Create()
        {
            var plan = new Plan();

            return plan;
        }

        public Plan SetDays(int days)
        {
            Days = days;

            return this;
        }

        public Plan SetDailyPrice(decimal dailyPrice) 
        {
            DailyPrice = dailyPrice;

            return this;
        }

        public Plan SetPenaltyPercentage(decimal penaltyPercentage)
        {
            PenaltyPercentage = penaltyPercentage;

            return this;
        }
    }
}
