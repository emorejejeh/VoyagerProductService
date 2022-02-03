using System;
using System.Collections.Generic;
using System.Linq;
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
        public decimal ExecuteProcess(List<string> products)
        {
            foreach(var p in products)
            {
                _productService.ScanProduct(p);
            }

            return _productService.CalculateTotal();
        }
    }
}
