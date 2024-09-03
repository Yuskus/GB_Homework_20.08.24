using AutoMapper;
using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel;
using HomeworkGB11.DatabaseModel.DTO;

namespace HomeworkGB11.Services
{
    public class EmployeeService(IMapper mapper, IEmployeesDbContext context) : IEmployeeService
    {
        private readonly IEmployeesDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public int AddEmployee(PutEmployeeDTO employee)
        {
            var element = _mapper.Map<EmployeeInfo>(employee);
            _context.Employees.Add(element);
            _context.SaveChanges();
            return element.Id;
        }

        public IEnumerable<GetEmployeeDTO> GetEmployees()
        {
            return [.. _context.Employees.Select(x => _mapper.Map<GetEmployeeDTO>(x)) ];
        }
    }
}
