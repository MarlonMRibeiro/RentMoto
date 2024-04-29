using Application.Dtos;
using Application.UseCases;
using Application.UseCases.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderUseCase _orderUseCase;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderUseCase orderUseCase, UserManager<User> userManager, ILogger<OrderController> logger)
        {
            _orderUseCase = orderUseCase;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public IActionResult CreateOrder([FromBody] CreateOrderDto data)
        {
            try
            {
                var response = _orderUseCase.CreateOrder(data);

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


        [HttpPost]
        [Route("TakeOrder")]
        public IActionResult TakeOrder(Guid orderId)
        {
            try
            {
                var response = _orderUseCase.TakeOrder(orderId, _userManager.GetUserAsync(User).Result.Id);

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

        [HttpPost]
        [Route("FinalizeOrder")]
        public IActionResult FinalizeOrder(Guid orderId)
        {
            try
            {
                var response = _orderUseCase.FinalizeOrder(orderId, _userManager.GetUserAsync(User).Result.Id);

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
