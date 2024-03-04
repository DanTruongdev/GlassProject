using Castle.Core.Internal;
using GlassECommerce.Data;
using GlassECommerce.DTOs;
using GlassECommerce.Models;
using GlassECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GlassECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _dbContext;

        public AdminController(IProductService productService, ApplicationDbContext dbContext)
        {
            _productService = productService;
            _dbContext = dbContext;
        }
        //Category
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var res = await _productService.GetAllCategories();
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }

        [HttpGet("categories/{categoryId}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int categoryId)
        {
            try
            {
                var res = await _productService.GetCategoryById(categoryId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }

        [HttpPost("categories/add")]
        public async Task<IActionResult> AddCategory([Required]string categoryName)
        {
            if (categoryName.Length < 2) return BadRequest("Category name must be greater than 1 character");
            try
            {
                var res = await _productService.AddCategory(categoryName);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }


        [HttpPut("categories/edit/{categoryId}")]
        public async Task<IActionResult> EditCategory([FromRoute] int categoryId, [Required] string categoryName)
        {
            if (categoryName.Length < 2) return BadRequest("Category name must be greater than 1 character");
            try
            {
                var res = await _productService.EditCategory(categoryId, categoryName);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }

        [HttpDelete("categories/remove/{categoryId}")]
        public async Task<IActionResult> EditCategory([FromRoute] int categoryId)
        {
            try
            {
                var res = await _productService.RemoveCategory(categoryId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }

        //Unit
        [HttpGet("units")]
        public async Task<IActionResult> GetAllUnits()
        {
            try
            {
                var res = await _productService.GetAllUnits();
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }
        }


        [HttpGet("units/{unitId}")]
        public async Task<IActionResult> GetUnitById([FromRoute] int unitId)
        {
            try
            {
                var res = await _productService.GetUnitById(unitId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }

        [HttpPost("units/add")]
        public async Task<IActionResult> AddUnit([Required] string unitName)
        {
            if (unitName.Length < 2) return BadRequest("Unit name must be greater than 1 character");
            try
            {
                var res = await _productService.AddUnit(unitName);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }


        [HttpPut("units/edit/{unitId}")]
        public async Task<IActionResult> EditUnit([FromRoute] int unitId, [Required] string unitName)
        {
            if (unitName.Length < 2) return BadRequest("Unit name must be greater than 1 character");
            try
            {
                var res = await _productService.EditUnit(unitId, unitName);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }


        [HttpDelete("units/remove/{unitId}")]
        public async Task<IActionResult> RemoveUnit([FromRoute] int unitId)
        {
            try
            {
                var res = await _productService.RemoveUnit(unitId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }
        }

        //Collection
        [HttpGet("collections")]
        public async Task<IActionResult> GetAllCollections()
        {
            try
            {
                var res = await _productService.GetAllCollections();
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }
        }

        [HttpGet("collections/{collectionId}")]
        public async Task<IActionResult> GetCollectionById([FromRoute] int collectionId)
        {
            try
            {
                var res = await _productService.GetCollectionById(collectionId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }


        [HttpPost("collections/add")]
        public async Task<IActionResult> AddCollection([FromBody] AddCollectionDTO model)
        {
            if (model.CollectionName.Length < 2) return BadRequest("Collection name must be greater than 1 character");
            try
            {
                var res = await _productService.AddCollection(model);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }

        [HttpPut("collections/edit/{collectionId}")]
        public async Task<IActionResult> EditCollection([FromRoute] int collectionId, [FromBody] AddCollectionDTO model)
        {
            if (model.CollectionName.Length < 2) return BadRequest("Collection name must be greater than 1 character");
            try
            {
                var res = await _productService.EditCollection(collectionId, model);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }


        [HttpDelete("collections/remove/{collectionId}")]
        public async Task<IActionResult> RemoveCollection([FromRoute] int collectionId)
        {
            try
            {
                var res = await _productService.RemoveCollection(collectionId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }
        }

        //Color
        [HttpGet("colors")]
        public async Task<IActionResult> GetAllColors()
        {
            try
            {
                var res = await _productService.GetAllColors();
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }
        }


        [HttpGet("colors/{colorId}")]
        public async Task<IActionResult> GetColorById([FromRoute] int colorId)
        {
            try
            {
                var res = await _productService.GetColorById(colorId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }

        [HttpPost("colors/add")]
        public async Task<IActionResult> AddColor([Required] string colorName)
        {
            if (colorName.Length < 2) return BadRequest("Color name must be greater than 1 character");
            try
            {
                var res = await _productService.AddColor(colorName);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }


        [HttpPut("colors/edit/{colorId}")]
        public async Task<IActionResult> EditColor([FromRoute] int colorId, [Required] string colorName)
        {
            if (colorName.Length < 2) return BadRequest("Color name must be greater than 1 character");
            try
            {
                var res = await _productService.EditColor(colorId, colorName);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }


        [HttpDelete("colors/remove/{colorId}")]
        public async Task<IActionResult> RemoveColor([FromRoute] int colorId)
        {
            try
            {
                var res = await _productService.RemoveColor(colorId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }
        }

        //Products
        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts([Required] int page)
        {
            try
            {
                var res = await _productService.GetAllProducts(page);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }
        }
        [HttpGet("products/{productId}")]
        public async Task<IActionResult> GetProductById([FromRoute] int productId)
        {
            try
            {
                var res = await _productService.GetProductById(productId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }
        }

        [HttpPost("products/add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO model)
        {
            Category categoryExist = await _dbContext.Categories.FindAsync(model.CategoryId);
            if (categoryExist == null) return NotFound(new Response("Error", $"The category with = {model.CategoryId} was not found"));
            foreach(int collectionId in model.CollectionIdList)
            {
                Collection collectionExist = await _dbContext.Collections.FindAsync(collectionId);
                if (collectionExist == null) return NotFound(new Response("Error", $"The collect with = {collectionId} was not found"));
            }
            if (!model.ProductCode.IsNullOrEmpty())
            {
                var productCodeExist = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductCode.ToUpper().Equals(model.ProductCode.ToUpper()));
                if (productCodeExist != null) return BadRequest(new Response("Error", $"The product code already exist"));
            }
            try
            {
                var res = await _productService.AddProduct(model);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }

        [HttpPut("products/edit/{productId}")]
        public async Task<IActionResult> EditProduct([FromRoute] int productId, [FromBody] ProductDTO model)
        {
            Product productExist = await _dbContext.Products.FindAsync(productId);
            if (productExist == null) return NotFound(new Response("Error", $"The product with = {productId} was not found"));
            Category categoryExist = await _dbContext.Categories.FindAsync(model.CategoryId);
            if (categoryExist == null) return NotFound(new Response("Error", $"The category with = {model.CategoryId} was not found"));
            foreach (int collectionId in model.CollectionIdList)
            {
                Collection collectionExist = await _dbContext.Collections.FindAsync(collectionId);
                if (collectionExist == null) return NotFound(new Response("Error", $"The collect with = {collectionId} was not found"));
            }
            if (!model.ProductCode.IsNullOrEmpty())
            {
                var productCodeExist = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductCode.ToUpper().Equals(model.ProductCode.ToUpper()));
                if (productCodeExist != null && !productCodeExist.ProductCode.Equals(model.ProductCode)) return BadRequest(new Response("Error", $"The product code already exist"));
            }
            try
            {
                var res = await _productService.EditProduct(productExist, model);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }

        [HttpDelete("products/remove/{productId}")]
        public async Task<IActionResult> RemoveProduct([FromRoute] int productId)
        {
            try
            {
                var res = await _productService.RemoveProduct(productId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }
        }

        //models
        [HttpGet("products/{productId}/models")]
        public async Task<IActionResult> GetAllModelsByProductId([FromRoute] int productId)
        {
            try
            {
                var res = await _productService.GetAllModelsByProductId(productId);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }
        }

        [HttpPost("products/{productId}/models/add")]
        public async Task<IActionResult> AddModel([FromRoute] int productId, [FromBody] AddModelDTO model)
        {
            Product productExist = await _dbContext.Products.FindAsync(productId);
            if (productExist == null) return NotFound(new Response("Error", $"The product with = {productId} was not found"));
            Unit unitExist = await _dbContext.Unit.FindAsync(model.UnitId);
            if (unitExist == null) return NotFound(new Response("Error", $"The unit with = {model.UnitId} was not found"));
            Color colorExist = await _dbContext.Colors.FindAsync(model.ColorId);
            if (colorExist == null) return NotFound(new Response("Error", $"The color with = {model.ColorId} was not found"));
            try
            {
                var res = await _productService.AddModel(productId, model);
                return res;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "An error occur when handle request"));
            }

        }
    }
}
