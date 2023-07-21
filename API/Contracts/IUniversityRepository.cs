using API.Models;

namespace API.Contracts
{
    public interface IUniversityRepository : ITableRepository<University>
    {
        IEnumerable<University> GetByName(string name);
    }
}
