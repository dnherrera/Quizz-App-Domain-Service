using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using QuizApp.API.Dtos;
using QuizApp.API.Models;
using QuizApp.API.Services;

namespace QuizApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
         private readonly ILogger<AuthController> _logger;
        private readonly IAuthRepository _authRepository;

        public AuthController(ILogger<AuthController> logger, IAuthRepository authRepository )
        {
            _logger = logger;
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromBody] UserForRegisterDto userFoRegister) 
        {
            //validate 

            userFoRegister.username = userFoRegister.username.ToLower();

            if (await _authRepository.UserExistsAsync(userFoRegister.username))
                ModelState.AddModelError("Username", $"Username {userFoRegister.username} already exists.");
               
            if (!ModelState.IsValid)
                 return BadRequest(ModelState);
                
            UserModel userToCreate = new UserModel 
            {
                Username = userFoRegister.username
            };

            UserModel createUser = await _authRepository.RegisterAsync(userToCreate, userFoRegister.password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] UserForLoginDto userForLogin)
        {
            UserModel userFromRepo = await _authRepository.LoginAsync(userForLogin.Username, userForLogin.Password);

            if (userFromRepo?.Id == null)
                return Unauthorized();

            // generate token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return Ok(new {tokenString});
        }
    }
}