using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Interface
{
    public interface IOrderNotificationUseCase
    {
        void ReceiveNotification(string message);

        IEnumerable<OrderNotification> GetAll();
    }
}
