using BallyTech.Infrastructure.Provider;
using BallyTech.Infrastructure.Types;
using BallyTech.UI.Web.Platform;
using System;
namespace Audit.Config
{
    public static class Configurator
    {
        public static void Configure()
        {
            BallyTech.Infrastructure.Provider.IServiceProvider iServiceProvider = new ServiceProvider(DIContainer.Init(), AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            PlatformAPIProxy.Init(iServiceProvider.Configuration);
        }
    }
}