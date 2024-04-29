using Application.Dtos;
using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Interface
{
    public interface IDeliveryManUseCase
    {
        ResponseViewModel CreateDeliveryMan(CreateDeliveryManDto newDeliveryMan, Guid userId);

        ResponseViewModel UpdateCnhFile(string cnhImage, Guid userId);
    }
}
