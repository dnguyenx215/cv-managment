using AutoMapper;
using CVManagementAPI.DTO;
using CVManagementAPI.Helpers.Page;
using CVManagementAPI.Models;
using CVManagementAPI.Repository;
using System.Linq.Expressions;

namespace CVManagementAPI.Services
{
    public class RoleService : IBaseService<Role, RoleDTO>
    {
        private readonly RoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IMapper mapper, IServiceProvider serviceProvider)
        {
            var _repositoryFactory = serviceProvider.GetService<RepositoryFactory>();
            _roleRepository = _repositoryFactory.RoleRepository;

            _mapper = mapper;
        }

        public async Task<List<RoleDTO>> ToListAsync()
        {
            var entities = await _roleRepository.ToListAsync();

            var roleListDTO = _mapper.Map<List<RoleDTO>>(entities).ToList();

            return roleListDTO;
        }

        public async Task<List<RoleDTO>> SearchAsync(Expression<Func<Role, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new BadHttpRequestException("Predicate is null!");
            }

            var searchResult = await _roleRepository.SearchAsync(predicate);
            var resultDTO = _mapper.Map<List<RoleDTO>>(searchResult);
            return resultDTO;
        }

        public async Task<RoleDTO> FindAsync(int id)
        {
            var entity = await _roleRepository.FindAsync(id);
            if (entity == null)
            {
                return new RoleDTO();
            }

            var roleDto = _mapper.Map<RoleDTO>(entity);

            if (roleDto == null)
            {
                return new RoleDTO();
            }

            return roleDto;
        }

        public async Task<int> InsertAsync(RoleDTO obj)
        {
            var existingRoleName = await _roleRepository.SelectAsync(r => r.RoleName == obj.RoleName);
            if (existingRoleName)
            {
                return -1;
            }

            var objMap = _mapper.Map<Role>(obj);
            return await _roleRepository.InsertAsync(objMap);
        }

        public async Task<int> UpdateAsync(int id, RoleDTO obj)
        {
            var existingEntity = await _roleRepository.SelectAsync(x => x.Id == id);
            if (!existingEntity)
            {
                return -1;
            }

            if (id != obj.Id)
            {
                return -1;
            }

            var updateEntity = _mapper.Map<Role>(obj);
            return await _roleRepository.UpdateAsync(updateEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var existingEntity = await _roleRepository.FindAsync(id);
            if (existingEntity == null)
            {
                return -1;
            }

            return await _roleRepository.DeleteAsync(existingEntity);
        }

        public Task<PageResult<Role>> GetAllPagination(Pagination? pagination)
        {
            throw new NotImplementedException();
        }
    }
}