using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entities
{
    public class Rental : BaseEntity
    {
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime EstimatedEndDate { get; private set; }

        [ForeignKey("Motorcycle")]
        public Guid MotorcycleId { get; private set; }
        public virtual Motorcycle Motorcycle { get; private set; }

        [ForeignKey("DeliveryMan")]
        public Guid DeliveryManId { get; private set; }
        public virtual DeliveryMan DeliveryMan { get; private set; }

        [ForeignKey("Plan")]
        public Guid PlanId { get; private set; }
        public virtual Plan Plan { get; private set; }

        public static Rental Create()
        {
            var rental = new Rental();

            return rental;
        }

        public Rental SetStartDate(DateTime startDate)
        {
            StartDate = startDate;

            return this;
        }

        public Rental SetEndDate(DateTime endDate)
        {
            EndDate = endDate;

            return this;
        }

        public Rental SetEstimatedEndDate(DateTime estimatedEndDate)
        {
            EstimatedEndDate = estimatedEndDate;

            return this;
        }

        public Rental SetMotorcycleId(Guid motorcycleId)
        {
            MotorcycleId = motorcycleId;

            return this;
        }

        public Rental SetDeliveryManId(Guid deliveryManId)
        {
            DeliveryManId = deliveryManId;

            return this;
        }

        public Rental SetPlanId(Guid planId)
        {
            PlanId = planId;

            return this;
        }

        public decimal CalculateRentalValue(Plan plan, DateTime deliveryManEndDate)
        {
            decimal totalValue = 0;

            if (deliveryManEndDate.Date == EstimatedEndDate.Date)
            {
                var planValue = plan.DailyPrice * plan.Days;
                totalValue += planValue;

            }
            else if (deliveryManEndDate.Date > EstimatedEndDate.Date)
            {
                var planValue =  plan.DailyPrice * plan.Days;

                var diffAdd = (deliveryManEndDate.Date - EstimatedEndDate.Date).Days;

                totalValue = planValue + (diffAdd * 50);
            }
            else
            {
                var daysUsed = (deliveryManEndDate.Date - StartDate.Date).Days;

                var daysNotUsed = (EstimatedEndDate.Date - deliveryManEndDate.Date).Days;

                var valueUsed = plan.DailyPrice * daysUsed;

                var dayliPenaltyValue = plan.DailyPrice + (plan.DailyPrice * plan.PenaltyPercentage);

                var penaltyValue = dayliPenaltyValue * daysNotUsed;

                totalValue = valueUsed + penaltyValue;
            }

            return totalValue;
        }
    }
}
