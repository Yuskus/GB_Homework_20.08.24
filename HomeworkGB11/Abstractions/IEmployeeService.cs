using HomeworkGB11.DatabaseModel.DTO;

namespace HomeworkGB11.Abstractions
{
    public interface IEmployeeService
    {
        IEnumerable<GetEmployeeDTO> GetEmployees();
        int AddEmployee(PutEmployeeDTO employee);
    }
}
