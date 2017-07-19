using System.ServiceModel;
using BallyTech.Infrastructure.Configuration;
using BallyTech.Infrastructure.Hosting;
using BallyTech.Platform.API.Contract;

namespace BallyTech.UI.Web.Platform
{
    public static partial class PlatformAPIProxy
    {
        private static IHostingClient hostingClient;

        /// <summary>
        /// Creates Asset API Service Instance.
        /// </summary>
        public static IAsset Asset
        {
            get
            {
                return hostingClient.GetProxy<IAsset>("assetEndPoint");
            }
        }

        /// <summary>
        /// Creates User Api Service Instance.
        /// </summary>
        public static IEmployee Employee
        {
            get
            {
                return hostingClient.GetProxy<IEmployee>("usersEndPoint");
            }
        }

        /// <summary>
        /// Creates HardCount Api Service Instance.
        /// </summary>
        public static IHardCount HardCount
        {
            get
            {
                return hostingClient.GetProxy<IHardCount>("hardCountEndPoint");
            }
        }

        //CODE REFACTORING
        public static IJackpotPayout JackpotPayout
        {
            get
            {
                return hostingClient.GetProxy<IJackpotPayout>("jackpotPayoutEndPoint");
            }
        }

        /// <summary>
        /// Creates Player Api Service Instance.
        /// </summary>
        public static IPlayer Player
        {
            get
            {
                return hostingClient.GetProxy<IPlayer>("playerEndPoint");
            }
        }

        /// <summary>
        /// Creates Report Api Service Instance.
        /// </summary>
        public static IReports Reports
        {
            get
            {
                return hostingClient.GetProxy<IReports>("reportsEndPoint");
            }
        }

        public static IPreCommitment PreCommitment
        {
            get
            {
                return hostingClient.GetProxy<IPreCommitment>("PreCommitmentEndPoint");
            }
        }

        /// <summary>
        /// Creates PreCommitment API Service Instance.
        /// </summary>
        public static IRating Rating
        {
            get
            {
                return hostingClient.GetProxy<IRating>("ratingEndPoint");
            }
        }

        /// <summary>
        /// Creates Drop Window Api Service Instance.
        /// </summary>
        public static ISiteGamingDay SiteGamingDay
        {
            get
            {
                return hostingClient.GetProxy<ISiteGamingDay>("dropEndPoint");
            }
        }

        public static ISlip Slip
        {
            get
            {
                return hostingClient.GetProxy<ISlip>("slipEndPoint");
            }
        }

        /// <summary>
        /// Creates IUILayoutMenuConfiguration API Service Instance.
        /// </summary>
        public static IUILayoutMenuConfiguration UILayoutMenuConfiguration
        {
            get
            {
                return hostingClient.GetProxy<IUILayoutMenuConfiguration>("uiLayoutMenuConfigurationEndPoint");
            }
        }

        /// <summary>
        /// Creates SoftCount Api Service Instance.
        /// </summary>
        public static ISoftCount SoftCount
        {
            get
            {
                return hostingClient.GetProxy<ISoftCount>("softCountEndPoint");
            }
        }

        public static ITicket Ticket
        {
            get
            {
                return hostingClient.GetProxy<ITicket>("ticketsEndPoint");
            }
        }

        public static IDirectedMessages DirectedMessages
        {
            get
            {
                return hostingClient.GetProxy<IDirectedMessages>("directedMessagesEndPoint");
            }
        }

        public static IAward Award
        {
            get
            {
                return hostingClient.GetProxy<IAward>("awardEndPoint");
            }
        }

        public static IInventoryManagement InventoryManagement
        {
            get
            {
                return hostingClient.GetProxy<IInventoryManagement>("inventoryManagement");
            }
        }

        public static ISiteRegistration SiteRegistration
        {
            get
            {
                return hostingClient.GetProxy<ISiteRegistration>("siteRegistrationEndPoint");
            }
        }

        public static IPlayerMasterDataDownloadService PlayerMasterDataDownload
        {
            get
            {
                return hostingClient.GetProxy<IPlayerMasterDataDownloadService>("PlayerMasterDataDownloadServiceEndPoint");
            }
        }

        public static IPOS POSService
        {
            get
            {
                return hostingClient.GetProxy<IPOS>("posEndPoint");
            }
        }

        public static IVisitor Visitor
        {
            get
            {
                return hostingClient.GetProxy<IVisitor>("visitorEndPoint");
            }
        }

        public static void Init(IConfigService configService)
        {
            var hostingProvider = new HostingProvider(configService);
            string uri = System.Configuration.ConfigurationManager.AppSettings.Get("PlatformURI");
            
            if (uri.Contains("{{ServerIP}}") == false)
            {
                configService.Save("HttpClient", "PlatformRESTApi", string.Format("http://{0}:9955/", uri));
            }

            var restHostingProvider = hostingProvider.GetRESTHostingProvider("PlatformRESTApi");
            hostingClient = restHostingProvider.GetHostingClient();
        }

        public static ISlotRevenueService SlotRevenueService
        {
            get
            {
                return hostingClient.GetProxy<ISlotRevenueService>("slotRevenueEndPoint");
            }
        }

        public static ISlotMachineService SlotMachineService
        {
            get
            {
                return hostingClient.GetProxy<ISlotMachineService>("slotMachineEndPoint");
            }
        }

        public static IPreference Preference
        {
            get
            {
                return hostingClient.GetProxy<IPreference>("preferenceEndPoint");
            }
        }

        /// <summary>
        /// Creates manualCashClearance Api Service Instance.
        /// </summary>
        public static IManualCashClearance ManualCashClearance
        {
            get
            {
                return hostingClient.GetProxy<IManualCashClearance>("manualcashclearanceEndPoint");
            }
        }
    }
}