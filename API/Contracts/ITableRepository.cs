namespace API.Contracts
{
    public interface ITableRepository<Table> where Table : class
    {
        IEnumerable<Table> GetAll();
        Table? GetByGuid(Guid guid);
        Table? Create(Table entity);
        bool Update(Table entity);
        bool Delete(Table entity);
    }
}
