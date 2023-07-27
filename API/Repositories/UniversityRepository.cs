using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UniversityRepository : TableRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingDbContext context) : base(context)
        {
        }

        public University? GetByCode(string code)
        {
            return _context.Set<University>().SingleOrDefault(u=> u.Code == code);
        }

        public IEnumerable<University> GetByName(string name)
        {
            return _context.Set<University>().Where(university => university.Name.Contains(name)).ToList();
        }

        bool IUniversityRepository.IsNotExist(string value)
        {
            return _context.Set<University>().SingleOrDefault(university=>university.Name.Contains(value)
                                            ||university.Code.Contains(value))is null;
        }
    }
}
