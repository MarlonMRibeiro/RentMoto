using Application.Dtos;
using Application.UseCases.Interface;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class MotorcycleController : Controller
    {
        private readonly IMotorcycleUseCase _motorcycleUseCase;
        private readonly ILogger<MotorcycleController> _logger;

        public MotorcycleController(IMotorcycleUseCase motorcycleUseCase, ILogger<MotorcycleController> logger)
        {
            _motorcycleUseCase = motorcycleUseCase;
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateMotorcycle")]
        public IActionResult CreateMotorcycle([FromBody] CreateMotorcycleDto data)
        {
            try
            {
                var response = _motorcycleUseCase.CreateMotorcycle(data);

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
        [Route("Get")]
        public IActionResult Get()
        {
            try
            {
                var response = _motorcycleUseCase.GetAllMotorcycle();

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetMotorcycleByPlate")]
        public IActionResult GetMotorcycleByPlate(string plate)
        {
            try
            {
                var response = _motorcycleUseCase.GetMotorcycleByPlate(plate);

                if (response == null)
                    return Ok(new ResponseViewModel(false, "Motorcycle not found"));

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(string plate)
        {
            try
            {
                var response = _motorcycleUseCase.DeleteMotorcycleByPlate(plate);

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

        [HttpPut]
        [Route("UpdateMotorcycle")]
        public IActionResult UpdateMotorcycle(UpdateMotorcyclePlateDto model)
        {
            try
            {
                var response = _motorcycleUseCase.UpdateMotorcyclePlate(model);

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
