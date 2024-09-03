using HomeworkGB11.DatabaseModel.DTO;

namespace HomeworkGB11.Abstractions
{
    public interface IWorkZoneService
    {
        IEnumerable<GetWorkZoneDTO> GetWorkZones();
        int AddWorkZone(PutWorkZoneDTO zone);
    }
}
