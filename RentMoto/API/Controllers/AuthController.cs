using Application.Dtos;
using Application.ViewModel;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nest;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IServiceProvider serviceProvider, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto data)
        {
            try
            {
                var response = await Create(data);

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
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateDto data)
        {
            try
            {
                if (data == null)
                    return BadRequest("Parameter cannot be null");

                var response = await Login(data);

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        public async Task<ResponseViewModel> Create(CreateUserDto newUser)
        {
            var user = Domain.Entities.User.Create();

            user
                .SetUserName(newUser.UserName)
                .SetName(newUser.Name)
                .SetEmail(newUser.Email);


            var response = await _userManager.CreateAsync(user, newUser.Password);

            if (!response.Succeeded)
            {
                var erros = response.Errors.Select(s => s.Description);
                return new ResponseViewModel(false, string.Join( "; ", erros));
            }

            if (newUser.Admin)
            {
                var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                var roleExist = await roleManager.RoleExistsAsync("Admin");

                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
                }

                await _userManager.AddToRoleAsync(user, "Admin");
            }

            if (!response.Succeeded) throw new Exception();

            return new ResponseViewModel(true, "Sucess to create user!");
        }

        public async Task<AuthenticateViewModel> Login(AuthenticateDto model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user == null)
                    throw new Exception("User or password wrongs");

                var response = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (!response.Succeeded)
                    throw new Exception("User or password wrongs");

                // return null if user not found
                if (user == null) return null;

                // authentication successful so generate jwt token
                var token = await GenerateToken(user);

                return new AuthenticateViewModel(user, token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<string> GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtToken").Value);


            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            var response = await _userManager.GetRolesAsync(user);

            foreach (var claim in response)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, claim));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }
}
