using AutoMapper;
using CVManagementAPI.DTO;
using CVManagementAPI.Helpers.Page;
using CVManagementAPI.Models;
using CVManagementAPI.Repository;
using System.Linq.Expressions;

namespace CVManagementAPI.Services
{
    public class SourceService : IBaseService<Source, SourceDTO>
    {
        private readonly SourceRepository _SourceRepository;
        private readonly IMapper _mapper;
        private CVRepository _cVRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="serviceProvider"></param>
        public SourceService(IMapper mapper, IServiceProvider serviceProvider)
        {
            var _repositoryFactory = serviceProvider.GetService<RepositoryFactory>();
            _SourceRepository = _repositoryFactory.SourceRepository;
            _cVRepository = _repositoryFactory.CVRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All source
        /// </summary>
        /// <returns>List<SourceDTO></returns>
        public async Task<List<SourceDTO>> ToListAsync()
        {
            var entities = await _SourceRepository.ToListAsync();
            var SourceListDTO = _mapper.Map<List<SourceDTO>>(entities).ToList();
            return SourceListDTO;
        }

        /// <summary>
        /// Search Source
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List<SourceDTO</returns>
        /// <exception cref="BadHttpRequestException"></exception>
        public async Task<List<SourceDTO>> SearchAsync(Expression<Func<Source, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new BadHttpRequestException("Predicate is null!");
            }

            var searchResult = await _SourceRepository.SearchAsync(predicate);
            var resultDTO = _mapper.Map<List<SourceDTO>>(searchResult);
            return resultDTO;
        }

        /// <summary>
        /// Find by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SourceDTO</returns>
        public async Task<SourceDTO> FindAsync(int id)
        {
            var entity = await _SourceRepository.FindAsync(id);
            if (entity == null)
            {
                return new SourceDTO();
            }

            var SourceDto = _mapper.Map<SourceDTO>(entity);
            if (SourceDto == null)
            {
                return new SourceDTO();
            }

            return SourceDto;
        }

        /// <summary>
        /// Insert source
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>int</returns>
        public async Task<int> InsertAsync(SourceDTO obj)
        {
            var existingSourceName = await _SourceRepository.SelectAsync(r => r.NameSource.ToLower().Equals(obj.NameSource.ToLower()));
            if (existingSourceName)
            {
                return -1;
            }

            var map = _mapper.Map<Source>(obj);
            return await _SourceRepository.InsertAsync(map);
        }

        /// <summary>
        /// Update Source
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns>int</returns>
        public async Task<int> UpdateAsync(int id, SourceDTO obj)
        {
            if (id != obj.Id)
            {
                return -1;
            }

            var existingEntity = await _SourceRepository.SelectAsync(x => x.Id == id);
            if (!existingEntity)
            {
                return -1;
            }

            if (obj.NameSource.ToLower().Equals("unknown") || obj.NameSource.ToLower().Equals("không xác định"))
            {
                return -2;
            }



            var updateEntity = _mapper.Map<Source>(obj);
            return await _SourceRepository.UpdateAsync(updateEntity);
        }

        /// <summary>
        /// delete source 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>int</returns>
        public async Task<int> DeleteAsync(int id)
        {
            var existingEntity = await _SourceRepository.FindAsync(id);
            if (existingEntity == null)
            {
                return -1;
            }

            if (existingEntity.NameSource.ToLower().Equals("unknown") || existingEntity.NameSource.ToLower().Equals("không xác định"))
            {
                return -2;
            }

            var cvsToUpdate = await _cVRepository.SearchAsync(c => c.SourceId == id);
            var unknownSource = await _SourceRepository.SearchAsync(s => s.NameSource.ToLower().Equals("unknown") || s.NameSource.ToLower().Equals("không xác định"));

            foreach (var cv in cvsToUpdate)
            {
                cv.SourceId = unknownSource[0].Id;
                await _cVRepository.UpdateAsync(cv);
            }

            return await _SourceRepository.DeleteAsync(existingEntity);
        }

        /// <summary>
        /// GetAllPagination
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns>PageResult<Source></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<PageResult<Source>> GetAllPagination(Pagination? pagination)
        {
            throw new NotImplementedException();
        }

    }
}