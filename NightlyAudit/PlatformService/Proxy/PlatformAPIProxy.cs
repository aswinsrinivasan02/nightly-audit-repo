using System.ServiceModel;
using BallyTech.Infrastructure.Configuration;
using BallyTech.Infrastructure.Hosting;
using NightlyAuditAPI.Contract;

namespace BallyTech.UI.Web.Platform
{
    public static partial class PlatformAPIProxy
    {
        private static IHostingClient hostingClient;

        /// <summary>
        /// Creates Asset API Service Instance.
        /// </summary>
        public static IApplicationHelper Asset
        {
            get
            {
                return hostingClient.GetProxy<IApplicationHelper>("applicationHelperEndPoint");
            }
        }

       

        public static void Init(IConfigService configService)
        {
            var hostingProvider = new HostingProvider(configService);
            string uri = System.Configuration.ConfigurationManager.AppSettings.Get("PlatformURI");
            
            if (uri.Contains("{{ServerIP}}") == false)
            {
                configService.Save("HttpClient", "NightlyAuditRESTApi", string.Format("http://{0}:9955/", uri));
            }

            var restHostingProvider = hostingProvider.GetRESTHostingProvider("NightlyAuditRESTApi");
            hostingClient = restHostingProvider.GetHostingClient();
        }

    }
}