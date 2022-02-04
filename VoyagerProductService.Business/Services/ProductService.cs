using System;
using System.Collections.Generic;
using System.Linq;
using VoyagerProductService.Business.Interfaces;
using VoyagerProductService.Dal.Interfaces;
using VoyagerProductService.Domain.Dtos;

namespace VoyagerProductService.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductData _productData;
        private decimal total;
        private readonly IEnumerable<ProductDto> products;
        private readonly IEnumerable<BulkPriceDto> bulkPrice;
        private List<ProductCounterDto> counter;
        public ProductService(IProductData productData)
        {
            _productData = productData;
            total = 0;
            products = _productData.GetAllProducts();
            bulkPrice = _productData.GetBulkPrices();
            counter = new List<ProductCounterDto>();
        }
        public decimal CalculateTotal()
        {
            return total;
        }

        public void ScanProduct(string productCode)
        {
            var currentProduct = products.FirstOrDefault(x => x.Code.Equals(productCode));
            if(currentProduct == null)
            {
                return;
            }
            CountProduct(productCode);
            total += currentProduct.Price;

            if (bulkPrice.Any(x => x.Code.Equals(productCode)))
            {
                var curBulkPrice = bulkPrice.FirstOrDefault(x => x.Code.Equals(productCode));
                if(counter.First(x => x.Code.Equals(productCode)).Quantity%curBulkPrice.Quantity==0)
                {
                    total += curBulkPrice.Price;
                    total -= (curBulkPrice.Quantity * currentProduct.Price);
                }
                
            }
        }

        public IEnumerable<BulkPriceDto> AddUpdateBulkPrices(List<BulkPriceDto> bulkPrices)
        {
            return _productData.AddUpdateBulkPrices(bulkPrices);
        }

        public IEnumerable<ProductDto> AddUpdateProducts(List<ProductDto> products)
        {
            return _productData.AddUpdateProducts(products);
        }

        public IEnumerable<BulkPriceDto> GetAllBulkPrices()
        {
            return _productData.GetBulkPrices();
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            return _productData.GetAllProducts();
        }

        public bool CheckIfValidProducts(List<string> lstProducts)
        {
            bool response = true;
            foreach(var p in lstProducts)
            {
                if (!products.Any(x => x.Code.Equals(p)))
                    return false;
            }
            return response;
        }

        private void CountProduct(string code)
        {
            if(counter.Any(x => x.Code.Equals(code)))
            {
                counter.First(x => x.Code.Equals(code)).Quantity++;
            }
            else
            {
                counter.Add(new ProductCounterDto { Code = code, Quantity = 1 });
            }    
            
        }
    }
}
