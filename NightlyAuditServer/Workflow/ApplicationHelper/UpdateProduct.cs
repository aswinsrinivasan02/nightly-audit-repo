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
    public class UpdateProduct : BallyTech.Infrastructure.Workflow.Task
    {

        public UpdateProduct(IWorkflowContext workflowContext)
            : base(workflowContext)
        {

        }

        protected override object GetResult()
        {
            return true;
        }

        public override string Name
        {
            get { return SG.NightlyAudit.DTO.DTOKey.UpdateProducts; }
        }

        protected override void Run(BallyTech.Infrastructure.Data.ITransactionAdapter transactionAdapter)
        {
            ProductDTO dto = this.workflowContext.GetInput<ProductDTO>();
            Products domain = ProductConvertor.GetProductDomain(dto);
            if (domain.Id == 0)
            {
                //NextId
                domain.ProductId = DIContainer.Instance.Get<IProductRepository>().NextID();
                domain.Added();
            }
            else
            {
                domain.Modified();
            }

            if (domain.IsDelete)
            {
                domain.Deleted();
            }

            IUnitOfWorkExecutor unitOfWorkExecutor = new UnitOfWorkExecutor();
            unitOfWorkExecutor.Add(new UnitOfWork<IProductRepository, Products, Int32>(DIContainer.Instance.Get<IProductRepository>(), domain, Operation.Add));
            unitOfWorkExecutor.Execute(transactionAdapter);

        }
    }
}
