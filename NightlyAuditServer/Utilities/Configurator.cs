using BallyTech.Infrastructure.Logging;
using BallyTech.Infrastructure.Provider;
using BallyTech.Infrastructure.Security;
using BallyTech.Infrastructure.Types;

namespace SG.NightlyAudit.Utilities
{
    public class Configurator
    {
        public static void Configure(DIContainer diContainer,IServiceProvider provider)
        {
            diContainer.Register<APISecurityProvider>(
              new APISecurityProvider(new OperationSecurityConfigProvider(), new SemanticLog("APISecurityProvider", provider.Utility.GetLogger()),
              new SessionManager(),
              new AuthorizationService(),
              new ExecutionContextFactory()));
        }
    }

    public class SessionManager : ISessionManager
    {
        public ISession Create(IUser user, IRole role, ILocation location, ISite site)
        {
            throw new System.NotImplementedException();
        }

        public IResponse<ISession> FindSession(string sessionId)
        {
            throw new System.NotImplementedException();
        }
    }

    public class AuthorizationService : IAuthorizer
    {

        public IResponse<IUserContext> Authorize(ISession sessionContext, IOperationSecurityConfig securityConfig)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ExecutionContextFactory : IExecutionContextFactory
    {

        public IExecutionContext Create(ISession sessionContext, IUserContext userContext, IOperationSecurityConfig securityConfig)
        {
            throw new System.NotImplementedException();
        }
    }

}
