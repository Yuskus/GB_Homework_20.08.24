using HomeworkGB11.DatabaseModel.DTO;

namespace HomeworkGB11.Abstractions
{
    public interface IPositionService
    {
        IEnumerable<GetPositionDTO> GetPositions();
        int AddPosition(PutPositionDTO position);
    }
}
