using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace NightlyAuditAPI.Contract
{
    [ServiceContract(Namespace = "http://www.SG.com/ApplicationHelper")]
    public interface IApplicationHelper
    {
        [OperationContract]
        [Operation("AuthenticateUser", OperationType.POST)]
        LoginResponseDTO AuthenticateUser(LoginRequestDTO loginRequestDTO);

        [OperationContract]
        [Operation("GetAllProducts", OperationType.GET)]
        List<ProductDTO> GetAllProducts();

        [OperationContract]
        [Operation("UpdateProduct", OperationType.POST)]
        bool UpdateProduct(ProductDTO productDTO);

        [OperationContract]
        [Operation("GetAudits", OperationType.GET)]
        List<AuditDTO> GetAudits(int auditId);

        [OperationContract]
        [Operation("SaveJob", OperationType.POST)]
        bool SaveJob(JobDTO jobDTO);
    }
}
