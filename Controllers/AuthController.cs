using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QuizApp.API.Dtos;
using QuizApp.API.Helpers;
using QuizApp.API.Models;
using QuizApp.API.Services;

namespace QuizApp.API.Controllers
{
    /// <summary>
    /// Auth Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthRepository _authRepository;
        private readonly string _tokenKey;

        /// <summary>
        /// Initializes a new instance of <seealso cref="AuthController"/>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="authRepository"></param>
        public AuthController(
            ILogger<AuthController> logger, 
            IAuthRepository authRepository,
            IConfiguration configuration)
        {
            _logger = logger;
            _authRepository = authRepository;
            _tokenKey = configuration.GetValue<string>("AppSetting:Token");
        }

        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="userFoRegister"></param>
        /// <returns>Status Code 201</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromBody] UserForRegisterDto userFoRegister) 
        { 
            // Check if the user already exists
            if (await _authRepository.IsUserExistsAsync(userFoRegister.Username.ToLower()))
                ModelState.AddModelError("Username", $"Username {userFoRegister.Username} already exists.");
            
            // return Bad Request
            if (!ModelState.IsValid)
                 return BadRequest(ModelState);

            var userToCreate = new UserModel();
            userToCreate.Username = userFoRegister.Username;

            await _authRepository.RegisterAsync(userToCreate, userFoRegister.Password);
            return StatusCode(201);
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="userForLogin"></param>
        /// <returns>Token String</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] UserForLoginDto userForLogin)
        {
            // Call the Login from the repo
            var userModel = await _authRepository.LoginAsync(userForLogin.Username, userForLogin.Password);

            if (userModel?.Id == null)
                return Unauthorized();

            // Generate Token from Request
            TokenString.GenerateTokenString(userModel.Id.ToString(), userModel.Username, _tokenKey, out string tokenString);

            return Ok( new {tokenString} );
        }
    }
}