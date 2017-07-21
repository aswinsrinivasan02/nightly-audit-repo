using BallyTech.Infrastructure.Workflow;

namespace NightlyAuditServer.Workflow
{
    public class SaveAuditJob : BallyTech.Infrastructure.Workflow.Task
    {
        private IWorkflowContext workflowContext;

        public SaveAuditJob(IWorkflowContext workflowContext)
            : base(workflowContext)
        {
            this.workflowContext = workflowContext;
        }

        protected override object GetResult()
        {
            return true;
        }

        public override string Name
        {
            get { return SG.NightlyAudit.DTO.DTOKey.SaveAuditJob; }
        }

        protected override void Run(BallyTech.Infrastructure.Data.ITransactionAdapter transactionAdapter)
        {
             
        }
    }
}
