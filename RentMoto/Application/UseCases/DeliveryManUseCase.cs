using Application.Dtos;
using Application.UseCases.Interface;
using Application.ViewModel;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class DeliveryManUseCase : IDeliveryManUseCase
    {
        private readonly IDeliveryManRepository _deliveryManRepository;
        public DeliveryManUseCase(IDeliveryManRepository deliveryManRepository)
        {
            _deliveryManRepository = deliveryManRepository;
        }

        public ResponseViewModel CreateDeliveryMan(CreateDeliveryManDto newDeliveryMan, Guid userId)
        {
            if (_deliveryManRepository.GetByUserId(userId) != null)
                return new ResponseViewModel(false, "Erro to create: DeliveryMan already create to this user");

            if (ExistDeliveryManByCnh(newDeliveryMan.CnhNumber))
                return new ResponseViewModel(false, "Erro to create: Existing cnh");

            if (ExistDeliveryManByCnpj(newDeliveryMan.Cnpj))
                return new ResponseViewModel(false, "Erro to create: Existing cnpj");

            var deliveryMan = DeliveryMan.Create();

            deliveryMan
                .SetUserId(userId)
                .SetBirth(newDeliveryMan.Birth)
                .SetCnpj(newDeliveryMan.Cnpj)
                .SetCnhNumber(newDeliveryMan.CnhNumber)
                .SetIdentity(newDeliveryMan.Identity)
                .SetCnhType(newDeliveryMan.CnhType);

            _deliveryManRepository.Add(deliveryMan);

            return new ResponseViewModel(true, "Success to create!");
        }

        public ResponseViewModel UpdateCnhFile(string cnhImage, Guid userId)
        {
            var deliveryMan = _deliveryManRepository.GetByUserId(userId);

            if (deliveryMan == null)
                return new ResponseViewModel(false, "Error: You need to be a DeliveryMan to do this");

            deliveryMan.SetCnhImage(cnhImage);

            _deliveryManRepository.Update(deliveryMan);

            return new ResponseViewModel(true, "Success to update file");
        }

        public bool ExistDeliveryManByCnh(string cnh)
        {
            var response = _deliveryManRepository.GetByCnh(cnh);

            return response != null;
        }

        public bool ExistDeliveryManByCnpj(string cnpj)
        {
            var response = _deliveryManRepository.GetByCnpj(cnpj);

            return response != null;
        }
    }
}
