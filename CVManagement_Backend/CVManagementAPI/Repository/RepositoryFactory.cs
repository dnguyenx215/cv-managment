using CVManagementAPI.Services;

namespace CVManagementAPI.Repository
{
    public class RepositoryFactory
    {
        public RoleRepository RoleRepository { get; set; }
        public PositionRepository PositionRepository { get; set; }
        public SourceRepository SourceRepository { get; set; }
        public CVRepository CVRepository { get; set; }

        public ColumnRelationshipRepository ColumnRelationshipRepository { get; set; }

        public ColumnLayoutRepository ColumnLayoutRepository { get; set; }


        public ColumnLayoutService ColumnLayoutService { get; set; }
        public ColumnRelationService ColumnRelationService { get; set; }
        public RepositoryFactory(CVManagementDbContext context, IServiceProvider serviceProvider)
        {
            RoleRepository = new RoleRepository(context);
            PositionRepository = new PositionRepository(context);

            SourceRepository = new SourceRepository(context);
            ColumnRelationshipRepository = new ColumnRelationshipRepository(context);
            ColumnLayoutRepository = new ColumnLayoutRepository(context);
            CVRepository = new CVRepository(context);
        }
    }
}
