using HomeworkGB12.Abstractions;
using HomeworkGB12.DatabaseModel.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkGB12.Controllers
{
    [ApiController]
    [Route("api_auth/[controller]")]
    public class LoginController(ILoginRepository userRepository) : ControllerBase
    {
        private readonly ILoginRepository _userRepository = userRepository;

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<string> Login([FromBody] LoginFormDTO loginForm)
        {
            try
            {
                var userToken = _userRepository.Authenticate(loginForm);
                return Ok(userToken);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
