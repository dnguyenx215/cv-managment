using CVManagementAPI.Models;

namespace CVManagementAPI.Repository
{
    public class PositionRepository : BaseRepository<Position>
    {
        public PositionRepository(CVManagementDbContext context) : base(context)
        {

        }
    }
}
