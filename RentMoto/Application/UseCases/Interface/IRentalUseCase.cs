using Application.Dtos;
using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Interface
{
    public interface IRentalUseCase
    {
        ResponseViewModel RentMotorcycle(Guid planId, Guid userId);
        ResponseViewModel ConsultRentalValue(Guid rentalId, DateTime endDate);
        ResponseViewModel ReturnMotorcycle(Guid userId);
    }
}
