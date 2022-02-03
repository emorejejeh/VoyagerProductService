using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using VoyagerProductService.Api.Controllers;
using VoyagerProductService.Business;
using VoyagerProductService.Business.BusinessLogics;
using VoyagerProductService.Business.Interfaces;

namespace VoyagerProductService.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private ProductController target;
        private IProduct product;
        private IProductService productService;
        private List<decimal> expectedResponse;
        int counter = 0;
        [SetUp]
        public void Setup()
        {
            expectedResponse = new List<decimal> { Convert.ToDecimal(13.25), Convert.ToDecimal(6), Convert.ToDecimal(7.25) };
            productService = new ProductService();
            product = new Product(productService);
            target = new ProductController(product);
        }

        private static readonly object[] _sourceLists =
        {
        new object[] {new List<string> { "A", "B", "C", "D", "A", "B", "A" } },   //case 1
        new object[] {new List<string> { "C", "C", "C", "C", "C", "C", "C" } }, //case 2
        new object[] {new List<string> { "A", "B", "C", "D" } } //case 2
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
    }
}