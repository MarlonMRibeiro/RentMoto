using Application.Dtos;
using Application.UseCases.Interface;
using Application.ViewModel;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class MotorcycleUseCase : IMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;

        public MotorcycleUseCase(IMotorcycleRepository motorcycleRepository, IRentalRepository rentalRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
        }


        public ResponseViewModel CreateMotorcycle(CreateMotorcycleDto newMotorcycle)
        {
            if (ExistsMotorcycleByPlate(newMotorcycle.LicensePlate))
                return new ResponseViewModel(false, "Error to create: Existing plate");

            var motorCycle = Motorcycle.Create();

            motorCycle.SetModel(newMotorcycle.Model)
                .SetIdentity(newMotorcycle.Identity)
                .SetLicensePlate(newMotorcycle.LicensePlate)
                .SetYear(newMotorcycle.Year)
                .ToMakeAvailable();


            _motorcycleRepository.Add(motorCycle);

            return new ResponseViewModel(true, "Success to create!");
        }


        public object GetAllMotorcycle()
        {
            var response = _motorcycleRepository.GetAll();

            return response;
        }

        public object GetMotorcycleByPlate(string plate)
        {
            var response = _motorcycleRepository.GetByPlate(plate);

            return response;
        }

        public ResponseViewModel DeleteMotorcycleByPlate(string plate)
        {
            var motorcycle = _motorcycleRepository.GetByPlate(plate);

            if(motorcycle == null)
                return new ResponseViewModel(false, "Error in deleting: motorcycle not found");

            if (motorcycle.Status == StatusMotorcycle.Rented)
                return new ResponseViewModel(false, "Error in deleting: motorcycle is rented");

            _motorcycleRepository.Remove(motorcycle);

            return new ResponseViewModel(true, "Success in deleting!");

        }

        public ResponseViewModel UpdateMotorcyclePlate(UpdateMotorcyclePlateDto model)
        {
            var motorcycle = _motorcycleRepository.GetByPlate(model.OldPlate);

            if (motorcycle == null)
                return new ResponseViewModel(false, "Error Motorcycle not found");

            motorcycle.SetLicensePlate(model.NewPlate);

            _motorcycleRepository.Update(motorcycle);

            return new ResponseViewModel(true, "Sucess to update motorcycle!");
        }

        private bool ExistsMotorcycleByPlate(string plate)
        {
            if(string.IsNullOrEmpty(plate))
                throw new Exception("Plate is null");

            var response = _motorcycleRepository.GetByPlate(plate);

            return response != null;
        }
    }
}
