using API.Contracts;
using API.DTOs.Roles;
using API.Models;

namespace API.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<GetViewRoleDto> GetAll()
        {
            var data = _repository.GetAll();
            if(data == null)
            {
                return Enumerable.Empty<GetViewRoleDto>();
            }
            var rolelist = new List<GetViewRoleDto>();
            foreach (var role in data)
            {
                rolelist.Add((GetViewRoleDto)role);
            }
            return rolelist;
        }
        public GetViewRoleDto? GetByGuid(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            if(data == null)
            {
                return null;
            }
            return (GetViewRoleDto?)data;
        }
        public GetViewRoleDto? Create(InsertRoleDto insertRoleDto)
        {
            var data = _repository.Create(insertRoleDto);
            if(data == null)
            {
                return null;
            }
            return (GetViewRoleDto?)data;
        }
        public int Update(GetViewRoleDto getViewRoleDto)
        {
            var data = _repository.GetByGuid(getViewRoleDto.Guid);
            if(data == null)
            {
                return -1;
            }
            Role toupdate = getViewRoleDto;
            toupdate.CreatedDate = data.CreatedDate;
            var result = _repository.Update(toupdate);
            return result ? 1:0;
        }
        public int Delete(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            if( data == null)
            {
                return -1;
            }
            var result = _repository.Delete(data);
            return result ? 1 : 0;

        }
    }
}
