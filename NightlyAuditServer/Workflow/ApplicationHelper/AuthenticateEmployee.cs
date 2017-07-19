using BallyTech.Infrastructure.Data;
using BallyTech.Infrastructure.Data.UnitOfWork;
using BallyTech.Infrastructure.Types;
using BallyTech.Infrastructure.Workflow;
using SG.NightlyAudit.Domain.Repository;
using SG.NightlyAudit.DTO;
using SG.NightlyAudit.Domain;
using System;

namespace NightlyAuditServer.Workflow
{
    public class AuthenticateUser : BallyTech.Infrastructure.Workflow.Task
    {
        IWorkflowContext workflowContext;
        LoginResponseDTO loginResponseDTO;

        public AuthenticateUser(IWorkflowContext workflowContext)
            : base(workflowContext)
        {
            this.workflowContext = workflowContext;
        }

        protected override dynamic GetResult()
        {
            return loginResponseDTO;
        }

        public override string Name
        {
            get { return SG.NightlyAudit.DTO.DTOKey.AuthenticateUser; }
        }

        protected override void Run(ITransactionAdapter transactionAdapter)
        {
            LoginRequestDTO requestDTO = this.workflowContext.GetInput<LoginRequestDTO>();
            loginResponseDTO = new LoginResponseDTO();
            loginResponseDTO.IsSuccess = true;
            AuditHistory auditHistory =  DIContainer.Instance.Get<IAuditHistoryRepository>().FindBy(1);
            auditHistory.AuditEndDateTime = DateTime.Now.AddDays(2);
            auditHistory.Modified();

            IUnitOfWorkExecutor unitOfWorkExecutor = new UnitOfWorkExecutor();
            unitOfWorkExecutor.Add(new UnitOfWork<IAuditHistoryRepository,AuditHistory,Int64>(DIContainer.Instance.Get<IAuditHistoryRepository>(),auditHistory,Operation.Add));
            unitOfWorkExecutor.Execute(transactionAdapter);
        }
    }
}
