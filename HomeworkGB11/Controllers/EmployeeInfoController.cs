using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkGB11.Controllers
{
    [ApiController]
    [Route("api_graphql/[controller]")]
    public class EmployeeInfoController(IEmployeeService service) : ControllerBase
    {
        private readonly IEmployeeService _service = service;

        [HttpGet("get")]
        public ActionResult Get()
        {
            try
            {
                var result = _service.GetEmployees().ToList();
                if (result is null) return StatusCode(404);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("post")]
        public ActionResult Post([FromBody] PutEmployeeDTO employee)
        {
            try
            {
                int result = _service.AddEmployee(employee);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
