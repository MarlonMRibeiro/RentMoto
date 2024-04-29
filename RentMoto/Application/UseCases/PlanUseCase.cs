using Application.UseCases.Interface;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class PlanUseCase : IPlanUseCase
    {
        private readonly IPlanRepository _planRepository;
        public PlanUseCase(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public IEnumerable<Plan> GetAllPlans()
        {
            var response = _planRepository.GetAll();

            return response;
        }


    }
}
