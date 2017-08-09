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

            string card = "%2291100002467?";
            int propIdLength = 3;
            int propIdStartPostion = 1;
            int cardTrack = 1;
            int accountLength = 9;
            int accountstart = 5;
            string[] cardTrackVal = card.Split(';');
            string trackval = cardTrackVal[cardTrack - 1];
            string propCode = trackval.Substring((propIdStartPostion - 1), propIdLength);
            string accountNumber = trackval.Substring((accountstart - 1), accountLength);

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
                Console.WriteLine("5. SaveJob");

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
                    case "5":
                        SaveJob();
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
         //   List<AuditJobDTO> auditJob = applicationHelper.GetAuditJob(auditId);
            
        }

        static void SaveJob()
        {
            JobDTO jobDTO = new JobDTO();
            jobDTO.AuditId = 1;
            jobDTO.ParametersChosen = "Casino,Location";
            ScheduleObject schObj = new ScheduleObject();
            schObj.ScheduleType = SchedulingTypes.OneTime;
            schObj.StartDateTime = DateTime.Now;
            jobDTO.ScheduleObject = schObj;
            applicationHelper.SaveJob(jobDTO);
        }

        


    }

}
