using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkGB11.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkZoneController(IWorkZoneService service) : ControllerBase
    {
        private readonly IWorkZoneService _service = service;

        [HttpGet("get")]
        public ActionResult Get()
        {
            try
            {
                var result = _service.GetWorkZones().ToList();
                if (result is null) return StatusCode(404);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("post")]
        public ActionResult Post([FromBody] PutWorkZoneDTO workZone)
        {
            try
            {
                int result = _service.AddWorkZone(workZone);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
