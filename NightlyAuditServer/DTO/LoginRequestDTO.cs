using System.Runtime.Serialization;

namespace SG.NightlyAudit.DTO
{
    [DataContract]
    public class LoginRequestDTO
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
