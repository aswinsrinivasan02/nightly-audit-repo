using SG.NightlyAudit.DTO;
using System.Collections.Generic;

namespace NightlyAuditAPI
{
    public partial class Service
    {
        public LoginResponseDTO AuthenticateUser(LoginRequestDTO loginRequestDTO)
        {
            return WorkflowInvoker.Invoke<LoginRequestDTO, LoginResponseDTO>(
                      "LoginUser",
                      this.workflowService, this.utility, loginRequestDTO);
        }

        public List<ProductDTO> GetAllProducts()
        {
            return WorkflowInvoker.Invoke<string, List<ProductDTO>>(
                                 "GetAllProducts",
                                 this.workflowService, this.utility, null);
        }

        public bool UpdateProduct(ProductDTO productDTO)
        {
            return WorkflowInvoker.Invoke<ProductDTO, bool>(
                "UpdateProduct",
                this.workflowService, this.utility, productDTO);
        }

        public List<AuditDTO> GetAuditJob(int jobId)
        {
            return WorkflowInvoker.Invoke<int, List<AuditDTO>>(
                         "GetAuditJob",
                         this.workflowService, this.utility, jobId);
        }

    }
}
