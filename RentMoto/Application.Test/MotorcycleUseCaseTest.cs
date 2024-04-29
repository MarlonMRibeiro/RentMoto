using Application.Dtos;
using Application.UseCases;
using Application.UseCases.Interface;
using Application.ViewModel;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Xunit;

namespace Application.Test
{
    public class MotorcycleUseCaseTest
    {

        [Fact]
        public void CreateMotorcycleNotCreatePlateExist()
        {
            var motorCycleRepository = new Mock<IMotorcycleRepository>();
            var rentalRepository = new Mock<IRentalRepository>();

            var motorcycle = new CreateMotorcycleDto()
            {
                Model = "Biz",
                Identity = "12345",
                LicensePlate = "AXW2345",
                Year = "2002"
            };

            motorCycleRepository.Setup(s => s.GetByPlate(motorcycle.LicensePlate)).Returns(new Motorcycle());

            var mock = new MotorcycleUseCase(motorCycleRepository.Object, rentalRepository.Object);

            var result = mock.CreateMotorcycle(motorcycle);

            var expected = new ResponseViewModel(false, "Error to create: Existing plate");

            Assert.Equal(expected.Success, result.Success);
            Assert.Equal(expected.Message, result.Message);
        }


    }
}