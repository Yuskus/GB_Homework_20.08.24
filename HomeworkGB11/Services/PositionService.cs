using AutoMapper;
using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel;
using HomeworkGB11.DatabaseModel.DTO;

namespace HomeworkGB11.Services
{
    public class PositionService(IMapper mapper, IEmployeesDbContext context) : IPositionService
    {
        private readonly IEmployeesDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public int AddPosition(PutPositionDTO position)
        {
            var element = _mapper.Map<EmployeePosition>(position);
            _context.EmployeesPosition.Add(element);
            _context.SaveChanges();
            return element.Id;
        }

        public IEnumerable<GetPositionDTO> GetPositions()
        {
            return [.. _context.EmployeesPosition.Select(x => _mapper.Map<GetPositionDTO>(x)) ];
        }
    }
}
