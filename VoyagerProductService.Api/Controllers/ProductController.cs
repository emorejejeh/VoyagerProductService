using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VoyagerProductService.Business.Interfaces;

namespace VoyagerProductService.Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }
        [HttpPost("Scan")]
        public ActionResult<decimal> ScanProducts([FromBody] IEnumerable<string> products)
        {
            return Ok(_product.ExecuteProcess(products.ToList()));
        }
    }
}
