using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment4.Tests;
using DatabaseService.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private IDataService _dataService;

        public ProductsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("category/{id}")]
        public ActionResult GetCategoriesByProductId(int id)
        {
            var products = _dataService.GetProductByCategory(id);
            if (products.Count == 0) return NotFound(products);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);
            if (product == null) return NotFound(product);
            
            return Ok(product);
        }

        [HttpGet("name/{searchString}")]
        public ActionResult GetProductByName(string searchString)
        {
            var products = _dataService.GetProductByName(searchString);
            if (products == null || products.Count == 0) return NotFound(products);

            return Ok(products);
        }
    }
}
