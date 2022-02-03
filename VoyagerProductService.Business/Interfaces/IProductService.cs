using System.Collections.Generic;

namespace VoyagerProductService.Business.Interfaces
{
    public interface IProductService
    {
        void ScanProduct(string productCode);
        decimal CalculateTotal();
    }
}
