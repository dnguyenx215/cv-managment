using CVManagementAPI.Models;

namespace CVManagementAPI.Repository
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(CVManagementDbContext context) : base(context)
        {
        }
    }
}