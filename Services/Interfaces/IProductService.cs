using GlassECommerce.DTOs;
using GlassECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GlassECommerce.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IActionResult> GetAllCategories();
        public Task<IActionResult> GetCategoryById(int categoryId);
        public Task<IActionResult> AddCategory(string categoryName);
        public Task<IActionResult> EditCategory(int categoryId, string categoryName);
        public Task<IActionResult> RemoveCategory(int categoryId);

        public Task<IActionResult> GetAllUnits();
        public Task<IActionResult> GetUnitById(int unitId);
        public Task<IActionResult> AddUnit(string unitName);
        public Task<IActionResult> EditUnit(int unitId, string unitName);
        public Task<IActionResult> RemoveUnit(int unitId);

        public Task<IActionResult> GetAllCollections();
        public Task<IActionResult> GetCollectionById(int collectionId);
        public Task<IActionResult> AddCollection(AddCollectionDTO model);
        public Task<IActionResult> EditCollection(int collectionId, AddCollectionDTO model);
        public Task<IActionResult> RemoveCollection(int collectionId);

        public Task<IActionResult> GetAllColors();
        public Task<IActionResult> GetColorById(int colorId);
        public Task<IActionResult> AddColor(string colorName);
        public Task<IActionResult> EditColor(int colorId, string colorName);
        public Task<IActionResult> RemoveColor(int colorId);



        public Task<IActionResult> GetAllProducts(int page);
        public Task<IActionResult> GetProductById(int productId);
        public Task<IActionResult> AddProduct(ProductDTO model);
        public Task<IActionResult> EditProduct(Product product, ProductDTO model);
        public Task<IActionResult> RemoveProduct(int productId);


        //public Task<IActionResult> GetAllModels(int page);
        public Task<IActionResult> GetAllModelsByProductId(int productId);
        public Task<IActionResult> AddModel(int productId, AddModelDTO model);
       
        //public Task<IActionResult> AddProduct(ProductDTO model);
        //public Task<IActionResult> EditProduct(Product product, ProductDTO model);
        //public Task<IActionResult> RemoveProduct(int productId);

    }
}
