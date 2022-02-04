using System;
using System.Collections.Generic;
using System.Linq;
using VoyagerProductService.Dal.Interfaces;
using VoyagerProductService.Domain.Dtos;

namespace VoyagerProductService.Dal
{
    public class ProductData : IProductData
    {
        private List<ProductDto> _products;
        private List<BulkPriceDto> _bulkPrices;
        public ProductData()
        {
            _products = CreateProducts() as List<ProductDto>;
            _bulkPrices = CreateBulkPrices() as List<BulkPriceDto>;
        }
        public IEnumerable<ProductDto> GetAllProducts()
        {
            return _products;
        }

        public IEnumerable<BulkPriceDto> GetBulkPrices()
        {
            return _bulkPrices;
        }

        public IEnumerable<BulkPriceDto> AddUpdateBulkPrices(List<BulkPriceDto> bulkPrices)
        {
            foreach(var b in bulkPrices)
            {
                if(_bulkPrices.Any(x => x.Code.Equals(b.Code))) //Update Existing
                {
                    _bulkPrices.First(x => x.Code.Equals(b.Code)).Price = b.Price;
                    _bulkPrices.First(x => x.Code.Equals(b.Code)).Quantity = b.Quantity;
                }
                else //Add new bulk price
                {
                    _bulkPrices.Add(b);
                }
            }
            return _bulkPrices;
        }

        public IEnumerable<ProductDto> AddUpdateProducts(List<ProductDto> products)
        {
            foreach (var p in products)
            {
                if (_products.Any(x => x.Code.Equals(p.Code))) //Update Existing
                {
                    _products.First(x => x.Code.Equals(p.Code)).Price = p.Price;
                    _products.First(x => x.Code.Equals(p.Code)).Name = p.Name;
                    _products.First(x => x.Code.Equals(p.Code)).Price = p.Price;
                }
                else //Add new bulk price
                {
                    _products.Add(p);
                }
            }
            return _products;
        }
        //Creating Data
        private IEnumerable<ProductDto> CreateProducts()
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

        private IEnumerable<BulkPriceDto> CreateBulkPrices()
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
    }
}
