using SG.NightlyAudit.Domain;
using SG.NightlyAudit.DTO;

namespace SG.NightlyAudit.DTODomainConvertor
{
    public static class ProductConvertor
    {
        public static ProductDTO GetProductDTO(Products productDomain)
        {
            ProductDTO dto = new ProductDTO();
            dto.ProductId = productDomain.Id;
            dto.ProductCode = productDomain.ProductCode;
            dto.ProductType = productDomain.ProductType;
            dto.IPInfo = productDomain.IPInfo;
            return dto;
        }

        public static Products GetProductDomain(ProductDTO dto)
        {
            Products domain = new Products(dto.ProductId);
            domain.ProductCode = dto.ProductCode;
            domain.ProductType = dto.ProductType;
            domain.IPInfo = dto.IPInfo;
            domain.IsDelete = dto.IsDelete;
            return domain;
        }
    }
}
