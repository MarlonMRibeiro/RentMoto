using Domain.Entities;
using System.Numerics;

namespace Domain.Test
{
    public class RentalTest
    {
        [Fact]
        public void CalculateTotalValueRentSevenDays()
        {
            var dateNow = DateTime.Now;
            var startDate = dateNow.AddDays(1);
            var estimatedEndDate = startDate.AddDays(7);

            var endDate = estimatedEndDate;

            var plan = Plan.Create().SetDays(7).SetDailyPrice(30).SetPenaltyPercentage((decimal)0.2);

            var rental = Rental.Create().SetStartDate(startDate).SetEstimatedEndDate(estimatedEndDate);

            Assert.Equal(210, rental.CalculateRentalValue(plan, endDate));
            Assert.Equal(240, rental.CalculateRentalValue(plan, endDate.AddDays(-5)));
            Assert.Equal(710, rental.CalculateRentalValue(plan, endDate.AddDays(10)));

        }

        [Fact]
        public void CalculateTotalValueRentFifthDays()
        {
            var dateNow = DateTime.Now;
            var startDate = dateNow.AddDays(1);
            var estimatedEndDate = startDate.AddDays(15);

            var endDate = estimatedEndDate;

            var plan = Plan.Create().SetDays(15).SetDailyPrice(28).SetPenaltyPercentage((decimal)0.4);

            var rental = Rental.Create().SetStartDate(startDate).SetEstimatedEndDate(estimatedEndDate);

            Assert.Equal(420, rental.CalculateRentalValue(plan, endDate));
            Assert.Equal(476, rental.CalculateRentalValue(plan, endDate.AddDays(-5)));
            Assert.Equal(920, rental.CalculateRentalValue(plan, endDate.AddDays(10)));
        }


        [Fact]
        public void CalculateTotalValueThirtyDays()
        {
            var dateNow = DateTime.Now;
            var startDate = dateNow.AddDays(1);
            var estimatedEndDate = startDate.AddDays(30);

            var endDate = estimatedEndDate;

            var plan = Plan.Create().SetDays(30).SetDailyPrice(22).SetPenaltyPercentage((decimal)0.6);

            var rental = Rental.Create().SetStartDate(startDate).SetEstimatedEndDate(estimatedEndDate);

            Assert.Equal(660, rental.CalculateRentalValue(plan, endDate));
            Assert.Equal(726, rental.CalculateRentalValue(plan, endDate.AddDays(-5)));
            Assert.Equal(1160, rental.CalculateRentalValue(plan, endDate.AddDays(10)));
        }
    }
}