using NightlyAuditServer;
using System;
using System.ServiceProcess;

namespace SG.NightlyAuditServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            string appLauch = (args == null || args.Length == 0) ? "WINDOWSSERVICE" : args[0].ToUpperInvariant();
            switch (appLauch)
            {
                case "WINDOWSSERVICE":
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[] 
            { 
                new NightlyAuditService() 
            };
                    ServiceBase.Run(ServicesToRun);
                    break;
                case "CONSOLESERVICE":
                default:
                    Console.WriteLine("...Platform Runtime Started...");

                    Console.WriteLine("Press <ENTER> to stop Platform Runtime.");

                    NightlyAuditService nightlyAuditService = new NightlyAuditService();
                    nightlyAuditService.Start();
                    Console.ReadLine();
                    Console.WriteLine("Stopping Platform Runtime...");

                    nightlyAuditService.Stop();
                    Console.WriteLine("Platform Runtime Stopped.");
                         Console.WriteLine("Press <ENTER> to exit.");
                        Console.ReadLine();
                        Environment.Exit(0);
                    break;


            }


        }
    }
}
