using BallyTech.Infrastructure.Data;
using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using SG.NightlyAudit.Domain.Repository;
using System;
using System.Collections.Generic;

namespace SG.NightlyAudit.Persistance
{
    public class ProductRepository : Repository<Products,Int32>,IProductRepository
    {
        BallyTech.Infrastructure.Provider.IServiceProvider provider;

        public ProductRepository(BallyTech.Infrastructure.Provider.IServiceProvider provider)
            : base(provider.Data.GetStorage())
        {
            this.provider = provider;
        }

        public override int NextID()
        {
            return this.provider.GetInt32IDGenerator("Product").Next();
        }

        public IList<Products> GetAllProducts()
        {
            return base.FindAll(new QueryCriteria() { QueryKey = "GetAllProducts" });
        }
    }
}
