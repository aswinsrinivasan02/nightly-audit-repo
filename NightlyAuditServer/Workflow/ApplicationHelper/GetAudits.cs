using BallyTech.Infrastructure.Types;
using BallyTech.Infrastructure.Workflow;
using SG.NightlyAudit.Domain;
using SG.NightlyAudit.Domain.Repository;
using SG.NightlyAudit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightlyAuditServer.Workflow
{
    public class GetAudits : BallyTech.Infrastructure.Workflow.Task
    {
        private IWorkflowContext workflowContext;
        private List<AuditDTO> auditJobList;

        public GetAudits(IWorkflowContext workflowContext)
            : base(workflowContext)
        {
            this.workflowContext = workflowContext;
            this.auditJobList = new List<AuditDTO>();
        }

        protected override object GetResult()
        {
            return auditJobList;
        }

        public override string Name
        {
            get { return SG.NightlyAudit.DTO.DTOKey.GetAudits; }
        }

        protected override void Run(BallyTech.Infrastructure.Data.ITransactionAdapter transactionAdapter)
        {
            int auditId = this.workflowContext.GetInput<int>();
            IAuditJobRepository auditJobRepository = DIContainer.Instance.Get<IAuditJobRepository>();
            List<AuditJob> auditJobsEntity = new List<AuditJob>();
            if (auditId == 0)
            {
                auditJobsEntity = auditJobRepository.GetAllAuditJobs();
            }
            else
            {
               auditJobsEntity.Add(auditJobRepository.FindBy(auditId));
            }

            foreach (var item in auditJobsEntity)
            {
                AuditDTO dto = new AuditDTO();
                dto.AuditId = item.AuditId;
                dto.AuditName = item.AuditName;
                dto.IsEnabled = item.IsEnabled;
                if (item.AuditParameters != null)
                {
                    foreach (var itemParam in item.AuditParameters)
                    {
                        AuditParametersDTO dtoParam = new AuditParametersDTO(itemParam.ParameterId, itemParam.ParameterType, itemParam.ParameterName);
                        dtoParam.ParameterValues = itemParam.ParameterValues;
                        dto.AuditParameters.Add(dtoParam);
                    }
                }
                this.auditJobList.Add(dto);
            }
        }
    }
}
