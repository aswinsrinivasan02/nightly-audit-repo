using BallyTech.Infrastructure.Types;
using BallyTech.Infrastructure.Workflow;
using SG.NightlyAudit.Domain;
using SG.NightlyAudit.Domain.Repository;
using SG.NightlyAudit.DTO;
using SG.NightlyAudit.DTODomainConvertor;
using System.Collections.Generic;
using System.Linq;

namespace NightlyAuditServer.Workflow
{
    public class GetAllProducts : BallyTech.Infrastructure.Workflow.Task
    {
        IWorkflowContext workflowContext;
        List<ProductDTO> productsDTO;

        public GetAllProducts(IWorkflowContext workflowContext)
            : base(workflowContext)
        {
            this.workflowContext = workflowContext;
        }
        protected override object GetResult()
        {
            return productsDTO;
        }

        public override string Name
        {
            get { return SG.NightlyAudit.DTO.DTOKey.GetAllProducts; }
        }

        protected override void Run(BallyTech.Infrastructure.Data.ITransactionAdapter transactionAdapter)
        {
            this.productsDTO = new List<ProductDTO>();
            List<Products> products = DIContainer.Instance.Get<IProductRepository>().GetAllProducts().ToList();
            foreach (var item in products)
            {
                this.productsDTO.Add(ProductConvertor.GetProductDTO(item));
            }
        }
    }
}
