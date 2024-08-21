using AutoMapper;
using CVManagementAPI.DTO;
using CVManagementAPI.Helpers.Page;
using CVManagementAPI.Models;
using CVManagementAPI.Repository;
using System.Linq.Expressions;

namespace CVManagementAPI.Services
{
    public class ColumnRelationService : IBaseService<ColumnRelationship, ColumnRelationshipDTO>
    {
        private readonly ColumnRelationshipRepository _ColumnRelationshipRepository;
        private readonly IMapper _mapper;

        public ColumnRelationService(IMapper mapper, IServiceProvider serviceProvider)
        {
            var _repositoryFactory = serviceProvider.GetService<RepositoryFactory>();
            _ColumnRelationshipRepository = _repositoryFactory.ColumnRelationshipRepository;
            _mapper = mapper;
        }

        public async Task<List<ColumnRelationshipDTO>> ToListAsync()
        {
            var entities = await _ColumnRelationshipRepository.ToListAsync();

            var ColumnRelationshipListDTO = _mapper.Map<List<ColumnRelationshipDTO>>(entities).ToList();

            return ColumnRelationshipListDTO;
        }

        public async Task<List<ColumnRelationshipDTO>> SearchAsync(Expression<Func<ColumnRelationship, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new BadHttpRequestException("Predicate is null!");
            }

            var searchResult = await _ColumnRelationshipRepository.SearchAsync(predicate);
            var resultDTO = _mapper.Map<List<ColumnRelationshipDTO>>(searchResult);
            return resultDTO;
        }

        public async Task<ColumnRelationshipDTO> FindAsync(int id)
        {
            var entity = await _ColumnRelationshipRepository.FindAsync(id);
            if (entity == null)
            {
                return new ColumnRelationshipDTO();
            }

            var ColumnRelationshipDto = _mapper.Map<ColumnRelationshipDTO>(entity);

            if (ColumnRelationshipDto == null)
            {
                return new ColumnRelationshipDTO();
            }
            return ColumnRelationshipDto;
        }

        public async Task<int> InsertAsync(ColumnRelationship obj)
        {
            var existingRelationship = await _ColumnRelationshipRepository.SelectAsync(r =>
            r.ColumnLayoutId == obj.ColumnLayoutId &&
            r.PullColumnId == obj.PullColumnId &&
            r.PutColumnId == obj.PutColumnId);

            if (existingRelationship)
            {
                return -1;
            }

            return await _ColumnRelationshipRepository.InsertAsync(obj);
        }

        public async Task<int> UpdateAsync(int id, ColumnRelationshipDTO obj)
        {
            var existingEntity = await _ColumnRelationshipRepository.SelectAsync(x => x.Id == id);
            if (!existingEntity)
            {
                throw new BadHttpRequestException("Entity not found.");
            }

            if (id != obj.Id)
            {
                throw new BadHttpRequestException("Mismatched Id.");
            }

            var updateEntity = _mapper.Map<ColumnRelationship>(obj);
            return await _ColumnRelationshipRepository.UpdateAsync(updateEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var existingEntity = await _ColumnRelationshipRepository.FindAsync(id);
            if (existingEntity == null)
            {
                throw new BadHttpRequestException("Entity not found.");
            }

            return await _ColumnRelationshipRepository.DeleteAsync(existingEntity);
        }

        public async Task<List<ColumnRelationshipDTO>> ToListAsyncPopulate()
        {
            var res = await _ColumnRelationshipRepository.ToListAsyncPopulate();
            var dtos = _mapper.Map<List<ColumnRelationshipDTO>>(res);
            return dtos;
        }

        public Task<int> InsertAsync(ColumnRelationshipDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<PageResult<ColumnRelationship>> GetAllPagination(Pagination? pagination)
        {
            throw new NotImplementedException();
        }
    }
}
