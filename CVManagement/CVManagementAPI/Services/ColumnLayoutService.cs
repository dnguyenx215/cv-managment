using AutoMapper;
using CVManagementAPI.DTO;
using CVManagementAPI.Helpers.Page;
using CVManagementAPI.Models;
using CVManagementAPI.Repository;
using System.Linq.Expressions;

namespace CVManagementAPI.Services
{
    public class ColumnLayoutService : IBaseService<ColumnLayout, ColumnLayoutDTO>
    {
        private readonly ColumnLayoutRepository _ColumnLayoutRepository;
        private readonly CVRepository _cVRepository;
        private readonly ColumnRelationshipRepository _columnRelationshipRepository;
        private readonly CVService _cVService;
        private readonly IMapper _mapper;

        public ColumnLayoutService(IMapper mapper, IServiceProvider serviceProvider)
        {
            var _repositoryFactory = serviceProvider.GetService<RepositoryFactory>();
            _cVService = new CVService(mapper, serviceProvider);
            _ColumnLayoutRepository = _repositoryFactory.ColumnLayoutRepository;
            _cVRepository = _repositoryFactory.CVRepository;
            _columnRelationshipRepository = _repositoryFactory.ColumnRelationshipRepository;
            _mapper = mapper;
        }

        public async Task<List<ColumnLayoutDTO>> ToListAsync()
        {
            var entities = await _ColumnLayoutRepository.ToListAsync();

            var ColumnLayoutListDTO = _mapper.Map<List<ColumnLayoutDTO>>(entities).ToList();

            return ColumnLayoutListDTO;
        }

        public async Task<List<ColumnLayoutDTO>> SearchAsync(Expression<Func<ColumnLayout, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new BadHttpRequestException("Predicate is null!");
            }

            var searchResult = await _ColumnLayoutRepository.SearchAsync(predicate);
            var resultDTO = _mapper.Map<List<ColumnLayoutDTO>>(searchResult);
            return resultDTO;
        }

        public async Task<ColumnLayoutDTO> FindAsync(int id)
        {
            var entity = await _ColumnLayoutRepository.FindAsync(id);
            if (entity == null)
            {
                return new ColumnLayoutDTO();
            }

            var ColumnLayoutDto = _mapper.Map<ColumnLayoutDTO>(entity);

            if (ColumnLayoutDto == null)
            {
                return new ColumnLayoutDTO();
            }
            return ColumnLayoutDto;
        }

        public async Task<int> InsertAsync(ColumnLayout obj)
        {
            var existingColumn = await _ColumnLayoutRepository.SelectAsync(r =>
            r.ColumnName == obj.ColumnName);

            //if (existingColumn)
            //{
            //    return -1;
            //}

            return await _ColumnLayoutRepository.InsertAsync(obj);
        }


        public async Task<int> UpdateAsync(int id, ColumnLayoutDTO obj)
        {
            var existingEntity = await _ColumnLayoutRepository.SelectAsync(x => x.Id == id);
            if (!existingEntity || id != obj.Id)
            {
                return -1;
            }
            if (obj.ColumnName.ToLower().Equals("unknown") || obj.ColumnName.ToLower().Equals("không xác định"))
            {
                return -2;
            }
            var updateEntity = _mapper.Map<ColumnLayout>(obj);

            return await _ColumnLayoutRepository.UpdateAsync(updateEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var existingEntity = await _ColumnLayoutRepository.FindAsync(id);
            if (existingEntity == null)
            {
                return -1;
            }

            if (existingEntity.ColumnName.ToLower().Equals("unknown") || existingEntity.ColumnName.ToLower().Equals("không xác định"))
            {
                return -2;
            }

            var relatedCVs = await _cVRepository.SearchAsync(c => c.ColumnId == id);
            var unknownColumn = await _ColumnLayoutRepository.SearchAsync(s => s.ColumnName.ToLower().Equals("unknown") || s.ColumnName.ToLower().Equals("không xác định"));

            var columnRelationships = await _columnRelationshipRepository.SearchAsync(cr => (cr.ColumnLayoutId == id) || (cr.PullColumnId == id) || (cr.PutColumnId == id));
            foreach (var cv in relatedCVs)
            {
                cv.ColumnId = unknownColumn[0].Id;
                await _cVRepository.UpdateAsync(cv);
            }

            foreach (var columnRelationship in columnRelationships)
            {
                columnRelationship.PullColumnId = unknownColumn[0].Id;
                columnRelationship.ColumnLayoutId = unknownColumn[0].Id;
                columnRelationship.PutColumnId = unknownColumn[0].Id;

                await _columnRelationshipRepository.UpdateAsync(columnRelationship);
            }

            return await _ColumnLayoutRepository.DeleteAsync(existingEntity);
        }

        public async Task<List<ColumnLayoutDTO>> ToListAsyncPopulate()
        {
            var entities = await _ColumnLayoutRepository.ToListAsyncPopulate();
            var ColumnLayoutListDTO = _mapper.Map<List<ColumnLayoutDTO>>(entities).ToList();

            foreach (ColumnLayoutDTO columnLayoutDto in ColumnLayoutListDTO)
            {
                foreach (CvDTO cvDTO in columnLayoutDto.CVs)
                {

                    var res = await _cVService.GetEmailTemplate(cvDTO.Status, cvDTO);
                    cvDTO.MailContent = res;
                }
            }
            return ColumnLayoutListDTO;
        }

        public async Task<int> InsertAsync(ColumnLayoutDTO obj)
        {
            var existingPositionName = await _ColumnLayoutRepository.SelectAsync(r => r.ColumnName == obj.ColumnName);
            //if (existingPositionName)
            //{
            //    return -1;
            //}

            var map = _mapper.Map<ColumnLayout>(obj);
            return await _ColumnLayoutRepository.InsertAsync(map);
        }

        public Task<PageResult<ColumnLayout>> GetAllPagination(Pagination? pagination)
        {
            throw new NotImplementedException();
        }
    }
}
