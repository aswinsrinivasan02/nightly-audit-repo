using System.Runtime.Serialization;

namespace SG.NightlyAudit.DTO
{
    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public int ProductType { get; set; }

        [DataMember]
        public string IPInfo { get; set; }

        [DataMember]
        public bool IsDelete { get; set; }
    }
}
