using CVManagementAPI.Models;

namespace CVManagementAPI.Repository
{
    public class SourceRepository : BaseRepository<Source>
    {
        public SourceRepository(CVManagementDbContext context) : base(context)
        {
        }
    }
}
