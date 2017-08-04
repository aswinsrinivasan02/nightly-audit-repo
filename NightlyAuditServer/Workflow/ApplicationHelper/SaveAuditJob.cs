using BallyTech.Infrastructure.Data;
using BallyTech.Infrastructure.Data.UnitOfWork;
using BallyTech.Infrastructure.Types;
using BallyTech.Infrastructure.Workflow;
using SG.NightlyAudit.Domain;
using SG.NightlyAudit.Domain.Repository;
using SG.NightlyAudit.DTO;
using SG.NightlyAudit.DTODomainConvertor;
using System;

namespace NightlyAuditServer.Workflow
{
    public class SaveJob : BallyTech.Infrastructure.Workflow.Task
    {
        private new IWorkflowContext workflowContext;
        private Job jobEntity;

        public SaveJob(IWorkflowContext workflowContext)
            : base(workflowContext)
        {
            this.workflowContext = workflowContext;
        }

        protected override object GetResult()
        {
            return jobEntity;
        }

        public override string Name
        {
            get { return SG.NightlyAudit.DTO.DTOKey.SaveAuditJob; }
        }

        protected override void Run(BallyTech.Infrastructure.Data.ITransactionAdapter transactionAdapter)
        {
            JobDTO jobDTO = this.workflowContext.GetInput<JobDTO>();
            jobEntity = JobConvertor.GetJobEntity(jobDTO);
            IJobRepository jobRepository = DIContainer.Instance.Get<IJobRepository>();

            if (jobEntity.JobId == 0)
            {
                jobEntity.JobId = jobRepository.NextID();
                jobEntity.Added();
            }
            else
            {
                jobEntity.Modified();
            }

            IUnitOfWorkExecutor unitOfWorkExecutor = new UnitOfWorkExecutor();
            unitOfWorkExecutor.Add(new UnitOfWork<IJobRepository, Job, Int64>(DIContainer.Instance.Get<IJobRepository>(), jobEntity, Operation.Add));
            unitOfWorkExecutor.Execute(transactionAdapter);
        }
    }
}
