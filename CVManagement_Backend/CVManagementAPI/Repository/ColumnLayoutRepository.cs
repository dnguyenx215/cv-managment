using CVManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CVManagementAPI.Repository
{
    public class ColumnLayoutRepository : BaseRepository<ColumnLayout>
    {
        private readonly CVManagementDbContext _context;
        public ColumnLayoutRepository(CVManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ColumnLayout>> ToListAsyncPopulate()
        {
            var res = await _context.Set<ColumnLayout>().Include(e => e.CVs.Where(cv => cv.DateReceiveCV >= DateTime.Now.AddMonths(-2))).ToListAsync(); ;
            return res;
        }
    }
}
