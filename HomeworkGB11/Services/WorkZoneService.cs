using AutoMapper;
using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel;
using HomeworkGB11.DatabaseModel.DTO;

namespace HomeworkGB11.Services
{
    public class WorkZoneService(IMapper mapper, IEmployeesDbContext context) : IWorkZoneService
    {
        private readonly IEmployeesDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public int AddWorkZone(PutWorkZoneDTO zone)
        {
            var element = _mapper.Map<WorkZone>(zone);
            _context.WorkZones.Add(element);
            _context.SaveChanges();
            return element.Id;
        }

        public IEnumerable<GetWorkZoneDTO> GetWorkZones()
        {
            return [.. _context.WorkZones.Select(x => _mapper.Map<GetWorkZoneDTO>(x)) ];
        }
    }
}
