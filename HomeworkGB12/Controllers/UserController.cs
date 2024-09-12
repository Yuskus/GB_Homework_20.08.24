using HomeworkGB12.Abstractions;
using HomeworkGB12.DatabaseModel.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkGB12.Controllers
{
    [ApiController]
    [Route("api_auth/[controller]")]
    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpPost("add_user")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<string> AddUser([FromBody] PutUserRightsDTO userRights)
        {
            try
            {
                int result = _userRepository.AddUser(userRights);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
