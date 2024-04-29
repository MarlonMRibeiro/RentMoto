using Application.UseCases.Interface;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class OrderNotificationController : Controller
    {
        private readonly IOrderNotificationUseCase _orderNotificationUseCase;
        private readonly ILogger<OrderNotificationController> _logger;

        public OrderNotificationController(IOrderNotificationUseCase orderNotificationUseCase, ILogger<OrderNotificationController> logger)
        {
            _orderNotificationUseCase = orderNotificationUseCase;
            _logger = logger;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            try
            {
                var response = _orderNotificationUseCase.GetAll();

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
