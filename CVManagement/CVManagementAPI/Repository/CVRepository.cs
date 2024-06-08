using CVManagementAPI.Models;

namespace CVManagementAPI.Repository
{
    public class CVRepository : BaseRepository<CV>
    {
        public CVRepository(CVManagementDbContext context) : base(context)
        {
        }
    }
}
