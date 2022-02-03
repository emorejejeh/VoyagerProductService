using System;
using System.Collections.Generic;
using System.Linq;
using VoyagerProductService.Business.Interfaces;
using VoyagerProductService.Domain.Dtos;

namespace VoyagerProductService.Business
{
    public class ProductService : IProductService
    {
        private decimal total;
        private readonly IEnumerable<ProductDto> products;
        private readonly IEnumerable<BulkPriceDto> bulkPrice;
        private List<ProductCounterDto> counter;
        public ProductService()
        {
            total = 0;
            products = GetAllProducts();
            bulkPrice = SetBulkPrice();
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
                if(curBulkPrice.Quantity.Equals(counter.First(x => x.Code.Equals(productCode)).Quantity))
                {
                    total += curBulkPrice.Price;
                    total -= (curBulkPrice.Quantity * currentProduct.Price);
                }
                
            }
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

        //Creating Data
        private IEnumerable<ProductDto> GetAllProducts()
        {
            return new List<ProductDto>
            {
                new ProductDto
                {
                    Code = "A",
                    Name = "Product A",
                    Price = Convert.ToDecimal(1.25)
                },
                new ProductDto
                {
                    Code = "B",
                    Name = "Product B",
                    Price = Convert.ToDecimal(4.25)
                },
                new ProductDto
                {
                    Code = "C",
                    Name = "Product C",
                    Price = Convert.ToDecimal(1.00)
                },
                new ProductDto
                {
                    Code = "D",
                    Name = "Product D",
                    Price = Convert.ToDecimal(0.75)
                }
            };
        }

        private IEnumerable<BulkPriceDto> SetBulkPrice()
        {
            return new List<BulkPriceDto>
            {
                new BulkPriceDto
                {
                    Code = "A",
                    Price = Convert.ToDecimal(3.00),
                    Quantity = 3
                },
                new BulkPriceDto
                {
                    Code = "C",
                    Price = Convert.ToDecimal(5.00),
                    Quantity = 6
                }
            };
        }

        private class ProductCounterDto
        {
            public string Code { get; set; }
            public int Quantity { get; set; }
        }
    }
}
