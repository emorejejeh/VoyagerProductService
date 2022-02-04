using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using VoyagerProductService.Api.Controllers;
using VoyagerProductService.Business;
using VoyagerProductService.Business.BusinessLogics;
using VoyagerProductService.Business.Interfaces;
using VoyagerProductService.Dal;
using VoyagerProductService.Dal.Interfaces;
using VoyagerProductService.Domain.Dtos;

namespace VoyagerProductService.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private ProductController target;
        private IProduct product;
        private IProductService productService;
        private IProductData productData;
        private List<decimal> expectedResponse;
        int counter = 0;
        [SetUp]
        public void Setup()
        {
            expectedResponse = new List<decimal> { Convert.ToDecimal(13.25), Convert.ToDecimal(6), Convert.ToDecimal(7.25), Convert.ToDecimal(6) };
            productData = new ProductData();
            productService = new ProductService(productData);
            product = new Product(productService);
            target = new ProductController(product);
        }

        private static readonly object[] _sourceLists =
        {
        new object[] {new List<string> { "A", "B", "C", "D", "A", "B", "A" } },   //case 1
        new object[] {new List<string> { "C", "C", "C", "C", "C", "C", "C" } }, //case 2
        new object[] {new List<string> { "A", "B", "C", "D" } }, //case 3
        new object[] {new List<string> { "A", "A", "A", "A", "A", "A" } } //case 3 Greater than 1 bulk reassign
        };

        [Test]
        [TestCaseSource("_sourceLists")]
        public void ProductService_ComputeTotal_ReturnCorrectTotal(List<string> products)
        {
            //Arrange
            
            //Act
            var response = target.ScanProducts(products);


            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<ActionResult<decimal>>(response);
                var objResult = response.Result as ObjectResult;
                Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);
                Assert.AreEqual(expectedResponse[counter], Convert.ToDecimal(objResult.Value));
            });

            counter++;
        }

        [Test]
        public void ProductService_HasInvalidProduct_ReturnBadRequest()
        {
            //Arrange
            List<string> products = new List<string> { "invalid", "A", "B" };
            //Act
            var response = target.ScanProducts(products);


            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<ActionResult<decimal>>(response);
                var objResult = response.Result as ObjectResult;
                Assert.AreEqual((int)HttpStatusCode.BadRequest, objResult.StatusCode);
            });
        }

        [Test]
        public void ProductService_GetAllProducts_ReturnAllProducts()
        {
            //Arrange
            //Act
            var response = target.GetAllProducts();


            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<ActionResult<IEnumerable<ProductDto>>>(response);
                var objResult = response.Result as ObjectResult;
                var responseValue = objResult.Value as List<ProductDto>;
                Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);
                Assert.AreEqual(4, responseValue.Count);
                Assert.AreEqual(productData.GetAllProducts().First().Code, responseValue.First().Code);
                Assert.AreEqual(productData.GetAllProducts().First().Name, responseValue.First().Name);
                Assert.AreEqual(productData.GetAllProducts().First().Price, responseValue.First().Price);
            });
        }

        [Test]
        public void ProductService_GetAllBulkPrices_ReturnAllProducts()
        {
            //Arrange
            //Act
            var response = target.GetAllBulkPrices();


            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<ActionResult<IEnumerable<BulkPriceDto>>>(response);
                var objResult = response.Result as ObjectResult;
                var responseValue = objResult.Value as List<BulkPriceDto>;
                Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);
                Assert.AreEqual(2, responseValue.Count);
                Assert.AreEqual(productData.GetBulkPrices().First().Code, responseValue.First().Code);
                Assert.AreEqual(productData.GetBulkPrices().First().Price, responseValue.First().Price);
                Assert.AreEqual(productData.GetBulkPrices().First().Quantity, responseValue.First().Quantity);
            });
        }
    }
}