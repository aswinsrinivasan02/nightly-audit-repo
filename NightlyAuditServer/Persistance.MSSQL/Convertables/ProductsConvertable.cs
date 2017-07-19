using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using System;
using System.Collections.Generic;

namespace SG.NightlyAudit.Persistence.MSSql.Convertables
{
    public class ProductsConvertable : IConvertable<Products>
    {
        public IEnumerable<IPersistable> ToPersistables(Products aggregate)
        {
            List<Products> products = new List<Products>() { aggregate };
            return products;
        }
    }
}
