using BallyTech.Infrastructure.Types;
using System;

namespace SG.NightlyAudit.Domain
{
    [Serializable]
    public class Products : Persistable,IAggregate<Int32>
    {
        public Int32 ProductId { get; set; }

        public String ProductCode { get; set; }

        public Int32 ProductType { get; set; }

        public string IPInfo { get; set; }

        public bool IsDelete { get; set; }

        public Products(Int32 productId)
        {
            this.ProductId = productId;
        }

        public int Id
        {
            get { return this.ProductId; }
        }
    }
}
