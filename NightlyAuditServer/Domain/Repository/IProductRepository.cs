using BallyTech.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.NightlyAudit.Domain.Repository
{
    public interface IProductRepository : IRepository<Products,Int32>
    {
        IList<Products> GetAllProducts();
    }
}
