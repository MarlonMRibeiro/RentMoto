using Application.Dtos;
using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Interface
{
    public interface IOrderUseCase
    {
        ResponseViewModel CreateOrder(CreateOrderDto newOrder);

        ResponseViewModel TakeOrder(Guid orderId, Guid userId);

        ResponseViewModel FinalizeOrder(Guid orderId, Guid userId);
    }
}
