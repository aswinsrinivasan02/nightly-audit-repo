using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
        [Operation("GetAuditJob", OperationType.GET)]
        List<AuditJobDTO> GetAuditJob(int auditId);
    }
}
