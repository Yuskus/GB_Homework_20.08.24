using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel.DTO;

namespace HomeworkGB11.Queries
{
    public class Query
    {
        public IEnumerable<GetEmployeeDTO> GetEmployees([Service] IEmployeeService service) => service.GetEmployees();
        public IEnumerable<GetPositionDTO> GetPosition([Service] IPositionService service) => service.GetPositions();
        public IEnumerable<GetWorkZoneDTO> GetWorkZone([Service] IWorkZoneService service) => service.GetWorkZones();
    }
}
