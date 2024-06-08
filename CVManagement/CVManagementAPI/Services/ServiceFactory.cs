using AutoMapper;
using CVManagementAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CVManagementAPI.Services
{
    public class ServiceFactory
    {
        public RoleService RoleService { get; set; }
        public AccountService AccountService { get; set; }
        public PositionService PositionService { get; set; }
        public SourceService SourceService { get; set; }
        public CVService CVService { get; set; }
        public ColumnLayoutService ColumnLayoutService { get; set; }
        public ColumnRelationService ColumnRelationService { get; set; }

        public MailService MailService { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="configuration"></param>
        public ServiceFactory(IMapper mapper, IServiceProvider serviceProvider,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            RoleService = new RoleService(mapper, serviceProvider);
            AccountService = new AccountService(mapper, serviceProvider, userManager, signInManager, configuration);
            PositionService = new PositionService(mapper, serviceProvider);
            SourceService = new SourceService(mapper, serviceProvider);
            CVService = new CVService(mapper, serviceProvider);
            ColumnLayoutService = new ColumnLayoutService(mapper, serviceProvider);
            ColumnRelationService = new ColumnRelationService(mapper, serviceProvider);
            MailService = new MailService();
        }

    }
}