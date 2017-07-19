using BallyTech.Infrastructure.Hosting;
using BallyTech.Infrastructure.Logging;
using BallyTech.Infrastructure.Provider;
using BallyTech.Infrastructure.Security;
using BallyTech.Infrastructure.Types;
using NightlyAuditAPI;
using NightlyAuditAPI.Contract;

namespace SG.NightlyAuditServer
{
    public class Configurator
    {
        public static void Configure(string configFilePath)
        {
            var diContainer = DIContainer.Init();
            IServiceProvider provider = new ServiceProvider(diContainer, configFilePath);
            provider.GroupName = System.Configuration.ConfigurationManager.AppSettings["GroupName"];
            diContainer.Register<IServiceProvider>(provider);
            SG.NightlyAudit.Utilities.Configurator.Configure(diContainer,provider);
            SG.NightlyAudit.Persistance.Configurator.Configure(diContainer,provider);

            IRESTHostingProvider restHosting = provider.HostingProvider.GetRESTHostingProvider("NightlyAuditRESTApi");
            IHostingServer hostingServer = restHosting.GetHostingServer();
            NightlyAuditAPI.Service serv = new NightlyAuditAPI.Service(provider.Configuration, provider.Utility, provider.Workflow);

            hostingServer.Register(typeof(IApplicationHelper), serv, "applicationHelperEndPoint");

            Runtime.Add("NightlyAuditRESTApi", hostingServer);

        }
    }
}
