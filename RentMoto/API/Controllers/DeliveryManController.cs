using API.FileUploadService;
using Application.Dtos;
using Application.UseCases.Interface;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize()]
    public class DeliveryManController : Controller
    {
        private readonly IDeliveryManUseCase _deliveryManUseCase;
        private readonly IFileUploadService _fileUploadService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DeliveryManController> _logger;
        public DeliveryManController(IDeliveryManUseCase deliveryManUseCase, UserManager<User> userManager, IFileUploadService fileUploadService, ILogger<DeliveryManController> logger)
        {
            _deliveryManUseCase = deliveryManUseCase;
            _userManager = userManager;
            _fileUploadService = fileUploadService;
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateDeliveryMan")]
        public IActionResult CreateDeliveryMan([FromBody]CreateDeliveryManDto data)
        {
            try
            {
                var response = _deliveryManUseCase.CreateDeliveryMan(data, _userManager.GetUserAsync(User).Result.Id);

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
        [Route("UpdateCnhFile")]
        public IActionResult UpdateCnhFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                    return BadRequest("File cannot be null or empty!");

                var fileExtension = file.FileName.Split(".")[1];

                if (fileExtension != "png" && fileExtension != "bmp")
                    return BadRequest("File extension not accept");

                string cnhImagem = null;
                if (file != null && file.Length > 0)
                {
                    cnhImagem = _fileUploadService.UploadFile(file);
                }

                var userId = _userManager.GetUserAsync(User).Result.Id;

                var response = _deliveryManUseCase.UpdateCnhFile(cnhImagem, userId);

                if(!response.Success)
                    return BadRequest(response);


                return Ok("Sucess");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

    }
}
