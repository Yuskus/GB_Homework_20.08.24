using HomeworkGB12.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkGB12.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController(IRoleRepository roleRepository) : ControllerBase
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        [HttpPost("add_role")]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddRole([FromBody] string role)
        {
            try
            {
                int result = _roleRepository.AddRole(role);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CheckForAdmin()
        {
            try
            {
                string? result = _roleRepository.CheckRole(HttpContext);
                return Ok(result ?? "Нет доступа.");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult CheckForUser()
        {
            try
            {
                string? result = _roleRepository.CheckRole(HttpContext);
                return Ok(result ?? "Нет доступа.");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
