using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel.DTO;

namespace HomeworkGB11.Mutations
{
    public class Mutation
    {
        public int AddEmployee([Service] IEmployeeService service, PutEmployeeDTO employee) => service.AddEmployee(employee);
        public int AddPosition([Service] IPositionService service, PutPositionDTO position) => service.AddPosition(position);
        public int AddWorkZone([Service] IWorkZoneService service, PutWorkZoneDTO workZone) => service.AddWorkZone(workZone);
    }
}
