using SG.NightlyAuditServer;
using System.Configuration;
using System.ServiceProcess;

namespace NightlyAuditServer
{
    public partial class NightlyAuditService : ServiceBase
    {
        public NightlyAuditService()
        {
            InitializeComponent();
            Configurator.Configure(
               ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
        }

        public void Start()
        {
            Runtime.StartHosts();
        }

        protected override void OnStart(string[] args)
        {
            this.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
