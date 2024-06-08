using AutoMapper;
using CVManagementAPI.DTO;
using CVManagementAPI.Helpers.Page;
using CVManagementAPI.Models;
using CVManagementAPI.Repository;
using System.Linq.Expressions;

namespace CVManagementAPI.Services
{
    public class PositionService : IBaseService<Position, PositionDTO>
    {
        private readonly PositionRepository _PositionRepository;
        private readonly CVRepository _cVRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="serviceProvider"></param>
        public PositionService(IMapper mapper, IServiceProvider serviceProvider)
        {
            var _repositoryFactory = serviceProvider.GetService<RepositoryFactory>();
            _PositionRepository = _repositoryFactory.PositionRepository;
            _cVRepository = _repositoryFactory.CVRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// function get all records in Position table
        /// </summary>
        /// <returns>List<PositionDTO></returns>
        public async Task<List<PositionDTO>> ToListAsync()
        {
            var entities = await _PositionRepository.ToListAsync();
            var PositionListDTO = _mapper.Map<List<PositionDTO>>(entities).ToList();
            return PositionListDTO;
        }

        /// <summary>
        /// Search Postion 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List<PositionDTO></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        public async Task<List<PositionDTO>> SearchAsync(Expression<Func<Position, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new BadHttpRequestException("Predicate is null!");
            }

            var searchResult = await _PositionRepository.SearchAsync(predicate);
            var resultDTO = _mapper.Map<List<PositionDTO>>(searchResult);
            return resultDTO;
        }

        /// <summary>
        /// Find by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>PositionDTO</returns>
        public async Task<PositionDTO> FindAsync(int id)
        {
            var entity = await _PositionRepository.FindAsync(id);
            if (entity == null)
            {
                return new PositionDTO();
            }

            var PositionDto = _mapper.Map<PositionDTO>(entity);

            if (PositionDto == null)
            {
                return new PositionDTO();
            }

            return PositionDto;
        }

        /// <summary>
        /// Insert Position
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>int</returns>
        public async Task<int> InsertAsync(PositionDTO obj)
        {
            var existingPositionName = await _PositionRepository.SelectAsync(r => r.PositionName.ToLower().Equals(obj.PositionName.ToLower()));

            if (existingPositionName)
            {
                return -1;
            }

            var map = _mapper.Map<Position>(obj);
            return await _PositionRepository.InsertAsync(map);
        }

        /// <summary>
        /// Update Position 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns>int</returns>
        public async Task<int> UpdateAsync(int id, PositionDTO obj)
        {
            if (id != obj.Id)
            {
                return -3;
            }
            var existingEntity = await _PositionRepository.SelectAsync(x => x.Id == id);
            if (!existingEntity)
            {
                return -1;
            }

            if (obj.PositionName.ToLower().Equals("unknown") || obj.PositionName.ToLower().Equals("không xác định"))
            {
                return -2;
            }



            var updateEntity = _mapper.Map<Position>(obj);
            return await _PositionRepository.UpdateAsync(updateEntity);
        }

        /// <summary>
        /// Delete the position, do not delete the CVs mapped to it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(int id)
        {
            var existingEntity = await _PositionRepository.FindAsync(id);
            if (existingEntity == null)
            {
                return -1;
            }

            if (existingEntity.PositionName.ToLower().Equals("unknown") || existingEntity.PositionName.ToLower().Equals("không xác định"))
            {
                return -2;
            }

            var cvsToUpdate = await _cVRepository.SearchAsync(c => c.PositionId == id);
            var unknownPos = await _PositionRepository.SearchAsync(s => s.PositionName.ToLower().Equals("unknown") || s.PositionName.ToLower().Equals("không xác định"));

            foreach (var cv in cvsToUpdate)
            {
                cv.PositionId = unknownPos[0].Id;
                await _cVRepository.UpdateAsync(cv);
            }

            return await _PositionRepository.DeleteAsync(existingEntity);
        }
        /// <summary>
        /// Get All Pagination
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<PageResult<Position>> GetAllPagination(Pagination? pagination)
        {
            throw new NotImplementedException();
        }

    }
}