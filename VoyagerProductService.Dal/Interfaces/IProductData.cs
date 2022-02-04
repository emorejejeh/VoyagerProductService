using System.Collections.Generic;
using VoyagerProductService.Domain.Dtos;

namespace VoyagerProductService.Dal.Interfaces
{
    public interface IProductData
    {
        IEnumerable<ProductDto> GetAllProducts();
        IEnumerable<BulkPriceDto> GetBulkPrices();
        IEnumerable<BulkPriceDto> AddUpdateBulkPrices(List<BulkPriceDto> bulkPrices);
        IEnumerable<ProductDto> AddUpdateProducts(List<ProductDto> products);
    }
}