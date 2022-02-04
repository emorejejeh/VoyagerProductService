using System.Collections.Generic;
using VoyagerProductService.Domain.Dtos;

namespace VoyagerProductService.Business.Interfaces
{
    public interface IProductService
    {
        void ScanProduct(string productCode);
        decimal CalculateTotal();
        bool CheckIfValidProducts(List<string> lstProducts);
        IEnumerable<BulkPriceDto> AddUpdateBulkPrices(List<BulkPriceDto> bulkPrices);
        IEnumerable<ProductDto> AddUpdateProducts(List<ProductDto> products);
        IEnumerable<BulkPriceDto> GetAllBulkPrices();
        IEnumerable<ProductDto> GetAllProducts();
    }
}
