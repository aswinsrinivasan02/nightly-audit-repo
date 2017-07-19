using System.Runtime.Serialization;

namespace SG.NightlyAudit.DTO
{
    [DataContract]
    public class LoginResponseDTO
    {
        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string EmpName { get; set; }
    }
}
