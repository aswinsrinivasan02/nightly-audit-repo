using BallyTech.Infrastructure.Security;
using BallyTech.Infrastructure.Types;
using System;

namespace SG.NightlyAudit.Utilities
{
    internal class OperationSecurityConfigProvider : IOperationSecurityConfigProvider
    {
        private BallyTech.Infrastructure.Provider.IServiceProvider serviceProvider = null;

        public IOperationSecurityConfig Get(string apiName)
        {
            serviceProvider = DIContainer.Instance.Get<BallyTech.Infrastructure.Provider.IServiceProvider>();
            OperationSecurity operationSecurity = null;
            try
            {
                operationSecurity = serviceProvider.Configuration.Get<OperationSecurity>("OperationSecurity", apiName);
            }
            catch
            {
                throw new ModuleException(0, String.Format("Security is not configured for API {0}.", apiName));
            }

            if (operationSecurity == null)
                throw new ModuleException(1, String.Format("Security is not configured for API {0}.", apiName));
            else
                return new OperationSecurityConfig { ID = operationSecurity.TaskId, OperationID = operationSecurity.OperationId, RequiresSession = operationSecurity.IsSessionRequired };
        }
    }

    internal class OperationSecurityConfig : IOperationSecurityConfig
    {
        public int? ID
        {
            get;
            set;
        }

        public int? OperationID
        {
            get;
            set;
        }

        public bool RequiresSession
        {
            get;
            set;
        }
    }
}
