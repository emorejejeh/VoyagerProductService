using System.Collections.Generic;
using VoyagerProductService.Domain.Dtos;

namespace VoyagerProductService.Business.Interfaces
{
    public interface IProduct
    {
        HttpResponse<decimal> ExecuteScan(List<string> products);
        HttpResponse<IEnumerable<BulkPriceDto>> AddUpdateBulkPrices(List<BulkPriceDto> bulkPrices);
        HttpResponse<IEnumerable<ProductDto>> AddUpdateProducts(List<ProductDto> products);
        HttpResponse<IEnumerable<BulkPriceDto>> GetAllBulkPrices();
        HttpResponse<IEnumerable<ProductDto>> GetAllProducts();

    }
}
