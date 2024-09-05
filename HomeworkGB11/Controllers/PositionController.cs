using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkGB11.Controllers
{
    [ApiController]
    [Route("api_graphql/[controller]")]
    public class PositionController(IPositionService service) : ControllerBase
    {
        private readonly IPositionService _service = service;

        [HttpGet("get")]
        public ActionResult Get()
        {
            try
            {
                var result = _service.GetPositions().ToList();
                if (result is null) return StatusCode(404);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("post")]
        public ActionResult Post([FromBody] PutPositionDTO position)
        {
            try
            {
                int result = _service.AddPosition(position);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
