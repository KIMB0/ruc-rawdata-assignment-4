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
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private IDataService _dataService;

        public CategoriesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/categories/
        /// <summary>
        /// Gets all Categories
        /// </summary>
        /// <returns>Categories</returns>
        [HttpGet]
        public ActionResult GetCategories()
        {
            var categories = _dataService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);
            if (category == null) return NotFound(category);

            return Ok(category);
        }

        [HttpPost]
        public ActionResult CreateCategory([FromBody] Category category)
        {
            var newCategory = _dataService.CreateCategory(category.Name, category.Description);
            return Created(newCategory.Id.ToString(), newCategory);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var categoryDeleted = _dataService.DeleteCategory(id);
            if (categoryDeleted == false) return NotFound();

            return Ok(id);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            var categoryUpdated = _dataService.UpdateCategory(id, category.Name, category.Description);
            if (categoryUpdated == false) return NotFound();

            return Ok(id);
        }
    }
}
