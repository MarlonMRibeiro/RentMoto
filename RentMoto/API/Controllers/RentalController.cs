using Application.Dtos;
using Application.UseCases.Interface;
using Application.ViewModel;
using Domain.Entities;
using Domain.Repositories;
using Infra.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RentalController : Controller
    {
        private readonly IRentalUseCase _rentalUseCase;
        private readonly IPlanUseCase _planUseCase;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RentalController> _logger;

        public RentalController(IRentalUseCase rentalUseCase, IPlanUseCase planUseCase, UserManager<User> userManager, ILogger<RentalController> logger)
        {
            _rentalUseCase = rentalUseCase;
            _planUseCase = planUseCase;
            _userManager = userManager;
            _logger = logger;
        }


        [HttpGet]
        [Route("GetPlans")]
        public IActionResult GetPlans()
        {
            try
            {
                var response = _planUseCase.GetAllPlans();

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("RentMotorcycle")]
        public IActionResult RentMotorcycle(Guid planId)
        {
            try
            {
                var response = _rentalUseCase.RentMotorcycle(planId, _userManager.GetUserAsync(User).Result.Id);

                if (!response.Success)
                    return BadRequest(response);

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ConsultRentalValue")]
        public IActionResult ConsultRentalValue(Guid rentalId, DateTime endDate)
        {
            try
            {
                var response = _rentalUseCase.ConsultRentalValue(rentalId, endDate);

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("ReturnMotorcycle")]
        public IActionResult ReturnMotorcycle(Guid motorcycleId)
        {
            try
            {
                var response = _rentalUseCase.ReturnMotorcycle(motorcycleId);

                if (!response.Success)
                    return BadRequest(response);

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

    }
}
