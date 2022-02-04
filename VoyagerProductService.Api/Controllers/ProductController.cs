using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using VoyagerProductService.Business.Interfaces;
using VoyagerProductService.Domain.Dtos;

namespace VoyagerProductService.Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }

        [HttpGet("Products")]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            var response = _product.GetAllProducts();
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                return BadRequest("Invalid Product");
            }
            return Ok(response.ResponseValue);
        }

        [HttpGet("BulkPrices")]
        public ActionResult<IEnumerable<BulkPriceDto>> GetAllBulkPrices()
        {
            var response = _product.GetAllBulkPrices();
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                return BadRequest("Invalid Product");
            }
            return Ok(response.ResponseValue);
        }

        [HttpPost("Scan")]
        public ActionResult<decimal> ScanProducts([FromBody] IEnumerable<string> products)
        {
            var response = _product.ExecuteScan(products.ToList());
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                return BadRequest("Invalid Product");
            }
            return Ok(response.ResponseValue);
        }

        [HttpPost("AddUpdateBulkPricing")]
        public ActionResult<IEnumerable<BulkPriceDto>> AddUpdateBulkPricing([FromBody] IEnumerable<BulkPriceDto> bulkPrices)
        {
            var response = _product.AddUpdateBulkPrices(bulkPrices.ToList());
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                return BadRequest("Invalid Product");
            }
            return Ok(response.ResponseValue);
        }

        [HttpPost("AddUpdateProducts")]
        public ActionResult<IEnumerable<ProductDto>> AddUpdateProducts([FromBody] IEnumerable<ProductDto> products)
        {
            var response = _product.AddUpdateProducts(products.ToList());
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                return BadRequest("Invalid Product");
            }
            return Ok(response.ResponseValue);
        }
    }
}
