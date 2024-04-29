using Application.Dtos;
using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Interface
{
    public interface IMotorcycleUseCase
    {
        ResponseViewModel CreateMotorcycle(CreateMotorcycleDto newMotorcycle);
        object GetAllMotorcycle();
        object GetMotorcycleByPlate(string plate);
        ResponseViewModel DeleteMotorcycleByPlate(string plate);
        ResponseViewModel UpdateMotorcyclePlate(UpdateMotorcyclePlateDto model);
    }
}
