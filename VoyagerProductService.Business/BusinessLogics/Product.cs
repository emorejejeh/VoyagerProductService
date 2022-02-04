using System.Collections.Generic;
using System.Linq;
using System.Net;
using VoyagerProductService.Business.Interfaces;
using VoyagerProductService.Domain.Dtos;

namespace VoyagerProductService.Business.BusinessLogics
{
    public class Product : IProduct
    {
        private readonly IProductService _productService;
        public Product(IProductService productService)
        {
            _productService = productService;
        }

        public HttpResponse<decimal> ExecuteScan(List<string> products)
        {
            if(!_productService.CheckIfValidProducts(products))
            {
                return new HttpResponse<decimal>(HttpStatusCode.BadRequest);
            }
            foreach(var p in products)
            {
                _productService.ScanProduct(p);
            }

            return new HttpResponse<decimal>(_productService.CalculateTotal());
        }

        public HttpResponse<IEnumerable<BulkPriceDto>> AddUpdateBulkPrices(List<BulkPriceDto> bulkPrices)
        {
            if (!_productService.CheckIfValidProducts(bulkPrices.Select(x =>x.Code).ToList()))
            {
                return new HttpResponse<IEnumerable<BulkPriceDto>>(HttpStatusCode.BadRequest);
            }
            return new HttpResponse<IEnumerable<BulkPriceDto>>(_productService.AddUpdateBulkPrices(bulkPrices));
        }

        public HttpResponse<IEnumerable<ProductDto>> AddUpdateProducts(List<ProductDto> products)
        {
            return new HttpResponse<IEnumerable<ProductDto>>(_productService.AddUpdateProducts(products));
        }

        public HttpResponse<IEnumerable<BulkPriceDto>> GetAllBulkPrices()
        {
            return new HttpResponse<IEnumerable<BulkPriceDto>>(_productService.GetAllBulkPrices());
        }

        public HttpResponse<IEnumerable<ProductDto>> GetAllProducts()
        {
            return new HttpResponse<IEnumerable<ProductDto>>(_productService.GetAllProducts());
        }
    }
}
