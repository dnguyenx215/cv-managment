using CVManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CVManagementAPI.Repository
{
    public class ColumnRelationshipRepository : BaseRepository<ColumnRelationship>
    {
        private readonly CVManagementDbContext _context;
        public ColumnRelationshipRepository(CVManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ColumnRelationship>> ToListAsyncPopulate()
        {
            var res = await _context.Set<ColumnRelationship>().Include(e => e.ColumnOwnRelationship).Include(e => e.PullColumn).Include(e => e.PutColumn).ToListAsync();
            return res;
        }
    }
}
