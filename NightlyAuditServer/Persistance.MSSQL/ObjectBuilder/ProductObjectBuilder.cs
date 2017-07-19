using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace SG.NightlyAudit.Persistence.MSSql.ObjectBuilder
{
    public class ProductObjectBuilder : IObjectBuilder<IDataReader,Products>
    {
        public IList<Products> Fill(IDataReader reader)
        {
            var productList = new List<Products>();
            int indexCount = 0;
            do
            {
                switch (indexCount)
                {
                    case 0:
                        this.FillValues(reader, productList);
                        break;

                    default:
                        break;
                }

                indexCount++;
            } while (reader.NextResult());

            return productList;

        }

        private void FillValues(IDataReader reader, List<Products> productList)
        {
            while (reader.Read())
            {
                Products product = new Products(reader.GetInt32(0));
                product.ProductCode = reader.GetString(1);
                product.ProductType = reader.GetInt32(2);
                product.IPInfo = reader.GetString(3);
                productList.Add(product);
            }
        }
    }
}
