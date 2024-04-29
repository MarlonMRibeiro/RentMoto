using Application.Dtos;
using Application.UseCases.Interface;
using Application.ViewModel;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class RentalUseCase : IRentalUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IPlanRepository _planRepository;
        public RentalUseCase(IRentalRepository rentalRepository, IDeliveryManRepository deliveryManRepository, IMotorcycleRepository motorcycleRepository, IPlanRepository planRepository)
        {
            _rentalRepository = rentalRepository;
            _deliveryManRepository = deliveryManRepository;
            _motorcycleRepository = motorcycleRepository;
            _planRepository = planRepository;

        }

        public ResponseViewModel RentMotorcycle(Guid planId, Guid userId)
        {
            try
            {
                var deliveryMan = _deliveryManRepository.GetByUserId(userId);

                if (deliveryMan == null)
                    return new ResponseViewModel(false, "Error to rent: DeliveryMan not found!");

                var motorCycle = _motorcycleRepository.GetAvaliableMotorcycle();

                if (motorCycle == null)
                    return new ResponseViewModel(false, "Error to rent: No one motorcycle is avaliable");

                if (deliveryMan.CnhType == Domain.Enums.TypeCnh.B)
                    return new ResponseViewModel(false, "Error to rent: The Deliveryman must have type A license!");

                var plan = _planRepository.GetById(planId);

                if (plan == null)
                    return new ResponseViewModel(false, "Error to rent: Plan not found!");

                var rental = Rental.Create();

                var estimatedDate = DateTime.UtcNow.AddDays(plan.Days + 1);

                rental
                    .SetStartDate(DateTime.UtcNow.AddDays(1))
                    .SetMotorcycleId(motorCycle.Id)
                    .SetDeliveryManId(deliveryMan.Id)
                    .SetEstimatedEndDate(estimatedDate)
                    .SetPlanId(plan.Id);


                _rentalRepository.Add(rental);

                motorCycle.Rent();

                _motorcycleRepository.Update(motorCycle);

                return new ResponseViewModel(true, "Success to rent");
            }
            catch (Exception e)
            {

                throw new Exception();
            }
        }

        public ResponseViewModel ConsultRentalValue(Guid rentalId, DateTime endDate)
        {
            var rental = _rentalRepository.GetById(rentalId);

            if(rental == null)
                return new ResponseViewModel(false, "Error to consult: Rental not found!");

            if(endDate < rental.StartDate)
                return new ResponseViewModel(false, "Error to consult: End date cannot be less or equal than start date!");


            var plan = _planRepository.GetById(rental.PlanId);

            var totalValue = rental.CalculateRentalValue(plan, endDate);

            return new ResponseViewModel(true, $"Total Value {ConvertToReais(totalValue)}");

        }

        public ResponseViewModel ReturnMotorcycle(Guid motorcycleId)
        {
            var rentalInProgress = _rentalRepository.GetInProgessByMotorcylceId(motorcycleId);

            if (rentalInProgress == null)
                return new ResponseViewModel(false, "Error to rent: There is no rental with this motorcycle in progress!");

            rentalInProgress.SetEndDate(DateTime.UtcNow);

            _rentalRepository.Update(rentalInProgress);

            var motorcycle = _motorcycleRepository.GetById(rentalInProgress.MotorcycleId);

            motorcycle.ToMakeAvailable();

            _motorcycleRepository.Update(motorcycle);

            return new ResponseViewModel(true, "Sucess to return Motorcycle");
        }

        public static string ConvertToReais(decimal value)
        {
            string formattedValue = "R$ " + value.ToString("#,##0.00");

            return formattedValue;
        }
    }
}
