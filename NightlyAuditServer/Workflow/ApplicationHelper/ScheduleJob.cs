using BallyTech.Infrastructure.Workflow;
using SG.NightlyAudit.Domain;
using SG.NightlyAudit.ScheduleEngine;

namespace NightlyAuditServer.Workflow
{
    public class ScheduleJob : BallyTech.Infrastructure.Workflow.Task
    {
        public ScheduleJob(IWorkflowContext workflowContext)
            : base(workflowContext)
        {

        }
        protected override object GetResult()
        {
            return true;
        }

        public override string Name
        {
            get { return SG.NightlyAudit.DTO.DTOKey.ScheduleJob; }
        }

        protected override void Run(BallyTech.Infrastructure.Data.ITransactionAdapter transactionAdapter)
        {
            Job jobEntity = base.GetValue("SCHEDULEJOB") as Job;
            ScheduleEngine.Instance.AddJobForSchedule(jobEntity);
        }
    }
}
