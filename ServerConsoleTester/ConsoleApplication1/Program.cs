using SG.NightlyAudit.DTO;
using NightlyAuditAPI.Contract;
using System.ServiceModel;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using BallyTech.Infrastructure.Types;
using BallyTech.Infrastructure.Hosting;
using System.Collections.Generic;
namespace ConsoleApplication1
{
    class Program
    {
        static IApplicationHelper applicationHelper;

        static void Main(string[] args)
        {

            BallyTech.Infrastructure.Provider.IServiceProvider iServiceProvider = new BallyTech.Infrastructure.Provider.ServiceProvider(DIContainer.Init(), AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            var hostingProvider = new HostingProvider(iServiceProvider.Configuration);
            string uri = System.Configuration.ConfigurationManager.AppSettings.Get("PlatformURI");

            if (uri.Contains("{{ServerIP}}") == false)
            {
                iServiceProvider.Configuration.Save("HttpClient", "PlatformRESTApi", string.Format("http://{0}:9955/", uri));
                
            }

            var restHostingProvider = hostingProvider.GetRESTHostingProvider("PlatformRESTApi");
            IHostingClient hostingClient = restHostingProvider.GetHostingClient();


            applicationHelper = hostingClient.GetProxy<IApplicationHelper>("applicationHelperEndPoint");
            bool isbreak = false;

            while (!isbreak)
            {
                Console.WriteLine("Please enter your choice");
                Console.WriteLine("1. GetAllProducts");
                Console.WriteLine("2. UpdateProducts");
                Console.WriteLine("3. GetAllAuditJobs");
                Console.WriteLine("4. GetAuditJobById");
                Console.WriteLine("99. Exit");

                string choice = Console.ReadLine();
                switch (choice.Trim())
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        GetAllAuditJobs();
                        break;
                    case "4":
                        Console.WriteLine("Enter AuditId");
                        int auditId = Convert.ToInt32(Console.ReadLine());
                        GetAllAuditJobs(auditId);
                        break;
                    case "99":
                        isbreak = true;
                        break;
                    default:
                        break;
                }
            }

        }

        static void GetAllAuditJobs(int auditId = 0)
        {
           // List<AuditJobDTO> auditJob = applicationHelper.GetAuditJob(auditId);
            
        }

        


    }

}
