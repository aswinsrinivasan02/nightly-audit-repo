using BallyTech.Infrastructure.Configuration;
using BallyTech.Infrastructure.Hosting;
using BallyTech.Infrastructure.Utilities;
using BallyTech.Infrastructure.Workflow;
using NightlyAuditAPI.Contract;
using System.ServiceModel;

namespace NightlyAuditAPI
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class Service : RESTController, IApplicationHelper
    {
        private readonly IConfigService configService;
        private readonly IUtilityProvider utility;
        private readonly IWorkflowService workflowService;

        public Service(IConfigService configService, IUtilityProvider utility, IWorkflowService workflowService)
        {
            this.configService = configService;
            this.utility = utility;
            this.workflowService = workflowService;
        }

        
    }
}
