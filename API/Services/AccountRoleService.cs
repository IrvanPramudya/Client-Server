using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;

namespace API.Services
{
    public class AccountRoleService
    {
        private readonly IAccountRoleRepository _repository;

        public AccountRoleService(IAccountRoleRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<GetViewAccountRoleDto> GetAll()
        {
            var data = _repository.GetAll();
            if(data == null)
            {
                return Enumerable.Empty<GetViewAccountRoleDto>();
            }
            var bookinglist = new List<GetViewAccountRoleDto>();
            foreach (var booking in data)
            {
                bookinglist.Add((GetViewAccountRoleDto)booking);
            }
            return bookinglist;
        }
        public GetViewAccountRoleDto? GetByGuid(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            if(data == null)
            {
                return null;
            }
            return (GetViewAccountRoleDto?)data;
        }
        public GetViewAccountRoleDto? Create(InsertAccountRoleDto dto)
        {
            var data = _repository.Create(dto);
            if( data == null)
            {
                return null;
            }
            return (GetViewAccountRoleDto?)data;
        }
        public int Update(GetViewAccountRoleDto dto)
        {
            var data = _repository.GetByGuid(dto.Guid);
            if(data == null)
            {
                return -1;
            }
            AccountRole toupdate = dto;
            toupdate.CreatedDate = data.CreatedDate;
            var result = _repository.Update(dto);
            return result ? 1 : 0;
        }
        public int Delete(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            if(data == null)
            {
                return -1;
            }
            var result = _repository.Delete(data);
            return result ? 1 : 0;
        }
    }
}
