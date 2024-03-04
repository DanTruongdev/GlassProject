using Castle.Core.Internal;
using GlassECommerce.Data;
using GlassECommerce.DTOs;
using GlassECommerce.Models;
using GlassECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace GlassECommerce.Services
{
    public class ProductService : ControllerBase, IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
    }
        //Category
        public async Task<IActionResult> AddCategory(string categoryName)
        {
            var categoryExist = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName.ToUpper().Equals(categoryName.ToUpper()));
            if (categoryExist != null) return BadRequest(new Response("Error", "This category name already exists"));
            Category newCategory = new Category()
            {
                CategoryName = categoryName
            };
            try
            {
                await _dbContext.AddAsync(newCategory);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new Response("Success", "Create category successfully"));
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when creating new category"));
            }
        }

        public async Task<IActionResult> EditCategory(int categoryId, string categoryName)
        {           
            Category editCategory = await _dbContext.Categories.FindAsync(categoryId);
            if (editCategory == null) return NotFound(new Response("Error", $"The category with id = {categoryId} was not found"));
            var categoryExist = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName.ToUpper().Equals(categoryName.ToUpper()));
            if (categoryExist != null && !categoryExist.CategoryName.Equals(categoryName)) return BadRequest(new Response("Error", "This category name already exists"));
            editCategory.CategoryName = categoryName;
            try
            {
                _dbContext.Update(editCategory);
                await _dbContext.SaveChangesAsync();
                return Ok($"update category with id = {categoryId} successfully");

            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when updating category"));
            }
        }

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            if (categories.IsNullOrEmpty()) return Ok(new List<Category>());
            var response = categories.Select(c => new
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToList();
            return Ok(response);
        }

        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            Category categoryExist = await _dbContext.Categories.FindAsync(categoryId);
            if (categoryExist == null) return NotFound();
            return Ok(new
            {
                CategoryId = categoryExist.CategoryId,
                CategoryName = categoryExist.CategoryName
            });
        }

        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            Category deleteCategory = await _dbContext.Categories.FindAsync(categoryId);
            if (deleteCategory == null) return NotFound(new Response("Error", $"The category with id = {categoryId} was not found"));
            if (!deleteCategory.Products.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response("Error", $"The category with id = {categoryId} cannot be deleted because there is product using this category"));
            }
            try
            {
                _dbContext.Remove(deleteCategory);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent,
                    new Response("Success", $"remove category with id = {categoryId} successfully"));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when remove category"));
            }

        }

       //Unit
        public async Task<IActionResult> GetAllUnits()
        {
            var units = await _dbContext.Unit.ToListAsync();
            if (units.IsNullOrEmpty()) return Ok(new List<Unit>());
            var response = units.Select(u => new
            {
                UnitId = u.UnitId,
                UnitName = u.UnitName
            }).ToList();
            return Ok(response);
        }

        public async Task<IActionResult> GetUnitById(int unitId)
        {
            Unit unitExist = await _dbContext.Unit.FindAsync(unitId);
            if (unitExist == null) return NotFound();
            return Ok(new
            {
                UnitId = unitExist.UnitId,
                UnitName = unitExist.UnitName
            });
        }

        public async Task<IActionResult> AddUnit(string unitName)
        {
            var unitExist = await _dbContext.Unit.FirstOrDefaultAsync(u => u.UnitName.ToUpper().Equals(unitName.ToUpper()));
            if (unitExist != null) return BadRequest(new Response("Error", "This unit name already exists"));
            Unit newUnit = new Unit()
            {
                UnitName = unitName
            };
            try
            {
                await _dbContext.AddAsync(newUnit);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new Response("Success", "Create new unit successfully"));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when creating new unit"));
            }
        }

        public async Task<IActionResult> EditUnit(int unitId, string unitName)
        {
            Unit editUnit = await _dbContext.Unit.FindAsync(unitId);
            if (editUnit == null) return NotFound(new Response("Error", $"The unit with id = {unitId} was not found"));
            var unitExist = await _dbContext.Unit.FirstOrDefaultAsync(u => u.UnitName.ToUpper().Equals(unitName.ToUpper()));
            if (unitExist != null & !unitExist.UnitName.Equals(unitName)) return BadRequest(new Response("Error", "This unit name already exists"));
            editUnit.UnitName = unitName;
            try
            {
                _dbContext.Update(editUnit);
                await _dbContext.SaveChangesAsync();
                return Ok($"update the unit with id = {unitId} successfully");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when updating unit"));
            }
        }

        public async Task<IActionResult> RemoveUnit(int unitId)
        {
            Unit deleteUnit = await _dbContext.Unit.FindAsync(unitId);
            if (deleteUnit == null) return NotFound(new Response("Error", $"The unit with id = {unitId} was not found"));
            if (!deleteUnit.Models.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response("Error", $"The unit with id = {unitId} cannot be deleted because there is model using this unit"));
            }
            try
            {
                _dbContext.Remove(deleteUnit);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent,
                    new Response("Success", $"remove unit with id = {unitId} successfully"));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when remove unit"));
            }
        }

      //Collection
        public async Task<IActionResult> GetAllCollections()
        {
            var collections = await _dbContext.Collections.ToListAsync();
            if (collections.IsNullOrEmpty()) return Ok(new List<Collection>());
            var response = collections.Select(c => new
            {
                CollectionId = c.CollectionId,
                CollectionName = c.CollectionName,
                Description = c.Description
            }).ToList();
            return Ok(response);
        }

        public async Task<IActionResult> GetCollectionById(int collectionId)
        {
            Collection collectionExist = await _dbContext.Collections.FindAsync(collectionId);
            if (collectionExist == null) return NotFound();
            return Ok(new
            {
                CollectionId = collectionExist.CollectionId,
                CollectionName = collectionExist.CollectionName,
                Description = collectionExist.Description
            });
        }

        public async Task<IActionResult> AddCollection(AddCollectionDTO model)
        {
            var collectionExist = await _dbContext.Collections.FirstOrDefaultAsync(c => c.CollectionName.ToUpper().Equals(model.CollectionName.ToUpper()));
            if (collectionExist != null) return BadRequest(new Response("Error", "This collection name already exists"));
            Collection newCollection = new Collection()
            {
                CollectionName = model.CollectionName,
                Description = model.Description.IsNullOrEmpty() ? "" : model.Description 
            };
            try
            {
                await _dbContext.AddAsync(newCollection);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new Response("Success", "Create new collection successfully"));
            }  catch   
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when creating new collection"));
            }
        }

        public async Task<IActionResult> EditCollection(int collectionId, AddCollectionDTO model)
        {
            Collection editCollection = await _dbContext.Collections.FindAsync(collectionId);
            if (editCollection == null) return NotFound(new Response("Error", $"The collection with id = {collectionId} was not found"));
            var collectionExist = await _dbContext.Collections.FirstOrDefaultAsync(u => u.CollectionName.ToUpper().Equals(model.CollectionName.ToUpper()));
            if (collectionExist != null && !collectionExist.Equals(model.CollectionName)) return BadRequest(new Response("Error", "This collection name already exists"));
            editCollection.CollectionName = model.CollectionName;
            editCollection.Description = model.Description;
            try
            {
                _dbContext.Update(editCollection);
                await _dbContext.SaveChangesAsync();
                return Ok($"update the collection with id = {collectionId} successfully");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when updating collection"));
            }
        }

        public async Task<IActionResult> RemoveCollection(int collectionId)
        {
            Collection deleteCollection = await _dbContext.Collections.FindAsync(collectionId);
            if (deleteCollection == null) return NotFound(new Response("Error", $"The collection with id = {collectionId} was not found"));
            if (!deleteCollection.CollectionProducts.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response("Error", $"The collection with id = {collectionId} cannot be deleted because there is product using this collection"));
            }
            try
            {
                _dbContext.Remove(deleteCollection);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent,
                    new Response("Success", $"remove the collection with id = {collectionId} successfully"));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when remove collection"));
            }
        }

        //Colors
        public async Task<IActionResult> GetAllColors()
        {
            var colors = await _dbContext.Colors.ToListAsync();
            if (colors.IsNullOrEmpty()) return Ok(new List<Color>());
            var response = colors.Select(c => new
            {
                ColorId = c.ColorId,
                ColorName = c.ColorName
            }).ToList();
            return Ok(response);
        }

        public async Task<IActionResult> GetColorById(int colorId)
        {
            Color colorExist = await _dbContext.Colors.FindAsync(colorId);
            if (colorExist == null) return NotFound();
            return Ok(new
            {
                ColorId = colorExist.ColorId,
                ColorName = colorExist.ColorName
            });
        }

        public async Task<IActionResult> AddColor(string colorName)
        {
            var colorExist = await _dbContext.Colors.FirstOrDefaultAsync(c => c.ColorName.ToUpper().Equals(colorName.ToUpper()));
            if (colorExist != null) return BadRequest(new Response("Error", "This color name already exists"));
            Color newColor = new Color()
            {
                ColorName = colorName
            };
            try
            {
                await _dbContext.AddAsync(newColor);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new Response("Success", "Create new color successfully"));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when creating new color"));
            }
        }

        public async Task<IActionResult> EditColor(int colorId, string colorName)
        {
            Color editColor = await _dbContext.Colors.FindAsync(colorId);
            if (editColor == null) return NotFound(new Response("Error", $"The color with id = {colorId} was not found"));
            var colorExist = await _dbContext.Colors.FirstOrDefaultAsync(u => u.ColorName.ToUpper().Equals(colorName.ToUpper()));
            if (colorExist != null && !colorExist.Equals(colorName)) return BadRequest(new Response("Error", "This color name already exists"));
            editColor.ColorName = colorName;
            try
            {
                _dbContext.Update(editColor);
                await _dbContext.SaveChangesAsync();
                return Ok($"update the color with id = {colorId} successfully");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when updating color"));
            }
        }

        public async Task<IActionResult> RemoveColor(int colorId)
        {
            Color deleteColor = await _dbContext.Colors.FindAsync(colorId);
            if (deleteColor == null) return NotFound(new Response("Error", $"The color with id = {colorId} was not found"));
            if (!deleteColor.Models.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response("Error", $"The color with id = {colorId} cannot be deleted because there is model using this color"));
            }
            try
            {
                _dbContext.Remove(deleteColor);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent,
                    new Response("Success", $"remove the color with id = {colorId} successfully"));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when remove color"));
            }
        }

        //Products
        public async Task<IActionResult> GetAllProducts(int page)
        {
            var products = await _dbContext.Products.Skip(page-1).Take(15).ToListAsync();
            if (products.IsNullOrEmpty()) return Ok(new List<Product>());
            var response = products.Select(p => new
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Category = new
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName
                },
                Collection = p.CollectionProducts.Select(cd => new
                {
                    CollectionId = cd.CollectionId,
                    CollectionName = cd.Collection.CollectionName
                }),
                VoteStar = p.VoteStar,
                Sold = p.Sold
            }).ToList();
            return Ok(response);
        }
        public async Task<IActionResult> GetProductById(int productId)
        {
            Product productExist = await _dbContext.Products.FindAsync(productId);
            if (productExist == null) return NotFound();
            return Ok(new
            {
                ProductId = productExist.ProductId,
                ProductName = productExist.ProductName,
                Category = new
                {
                    CategoryId = productExist.CategoryId,
                    CategoryName = productExist.Category.CategoryName
                },
                Collection = productExist.CollectionProducts.Select(cd => new
                {
                    CollectionId = cd.CollectionId,
                    CollectionName = cd.Collection.CollectionName
                }),
                VoteStar = productExist.VoteStar,
                Sold = productExist.Sold
            });
        }
        public async Task<IActionResult> AddProduct(ProductDTO model)
        {
            try
            {
                Product newProduct = new Product()
                {
                    ProductCode = model.ProductCode.IsNullOrEmpty() ? "" : model.ProductCode,
                    ProductName = model.ProductName,
                    CategoryId = model.CategoryId,
                    VoteStar = 0,
                    Sold = 0
                };

                await _dbContext.AddAsync(newProduct);
                await _dbContext.SaveChangesAsync();
               
                foreach (int collectionId in model.CollectionIdList) 
                {

                    CollectionProduct newCollectionProduct = new CollectionProduct()
                    {
                        CollectionId = collectionId,
                        ProductId = newProduct.ProductId
                    };
                    await _dbContext.AddAsync(newCollectionProduct);
                }

                await _dbContext.SaveChangesAsync();
                return Created("Create new product successfully", new
                {
                    ProductId = newProduct.ProductId,
                    ProductCode = newProduct.ProductCode,
                    ProductName = newProduct.ProductName,
                    CategoryId = newProduct.CategoryId,
                    Collection = newProduct.CollectionProducts.Select(c => new
                    {
                        CollectionId = c.CollectionId,
                        CollectionName = c.Collection.CollectionName
                    }),
                    VoteStar = 0,
                    Sold = 0
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when create new product"));
            }
        }
        public async Task<IActionResult> EditProduct(Product product, ProductDTO model)
        {
            try
            {

                product.ProductCode = model.ProductCode.IsNullOrEmpty() ? "" : model.ProductCode;
                product.ProductName = model.ProductName;
                product.CategoryId = model.CategoryId;
                _dbContext.Update(product);
                var collectionProductList = await _dbContext.CollectionProducts.Where(c => c.ProductId == product.ProductId).ToListAsync();
                foreach(var collectionProduct in collectionProductList)
                {
                    _dbContext.Remove(collectionProduct);
                }
                await _dbContext.SaveChangesAsync();

                foreach (int collectionId in model.CollectionIdList)
                {

                    CollectionProduct newCollectionProduct = new CollectionProduct()
                    {
                        CollectionId = collectionId,
                        ProductId = product.ProductId
                    };
                    await _dbContext.AddAsync(newCollectionProduct);
                }

                await _dbContext.SaveChangesAsync();
                return Ok(new
                {
                    ProductId = product.ProductId,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    Collection = product.CollectionProducts.Select(c => new
                    {
                        CollectionId = c.CollectionId,
                        CollectionName = c.Collection.CollectionName
                    }),
                    VoteStar = product.VoteStar,
                    Sold = product.Sold
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when update product"));
            }
        }
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            Product deleteProduct = await _dbContext.Products.FindAsync(productId);
            if (deleteProduct == null) return NotFound(new Response("Error", $"The product with id = {productId} was not found"));
            
            try
            {
                deleteProduct.Feedbacks.Clear();
                deleteProduct.CollectionProducts.Clear();
                deleteProduct.Models.Clear();
                _dbContext.Remove(deleteProduct);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent,
                    new Response("Success", $"remove the product with id = {productId} successfully"));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when remove product"));
            }
        }

        //Model
        public async Task<IActionResult> GetAllModelsByProductId(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null) return BadRequest(new Response("Error", $"The product with id = {productId} was not found"));
            var models = product.Models.ToList();
            if (models.IsNullOrEmpty()) return Ok(new List<Model>());
            var response = models.Select(m => new
            {
                ModelId = m.UnitId,
                ProductId = m.ProductId,
                UnitId = m.UnitId,
                ColorId = m.ColorId,
                Specification = m.Specification,
                PrimaryPrice = m.PrimaryPrice,
                SecondaryPrice = m.SecondaryPrice,
                Available = m.Available,
                Description = m.Description,
                Attachments = m.ModelAttachments.Select(a => new
                {
                    Path = a.Path,
                    Type = a.Type
                })
            }).ToList();
            return Ok(response);
        }

        public async Task<IActionResult> AddModel(int productId, AddModelDTO model)
        {
            try
            {
                Model newModel = new Model()
                {
                    ProductId = productId,
                    UnitId = model.UnitId,
                    ColorId = model.ColorId,
                    Specification = model.Specification.IsNullOrEmpty() ? "" : model.Specification,
                    PrimaryPrice = model.PrimaryPrice,
                    SecondaryPrice = model.SecondaryPrice,
                    Available = 0,
                    Description = model.Description.IsNullOrEmpty() ? "" : model.Description
                };
                await _dbContext.AddAsync(newModel);
                await _dbContext.SaveChangesAsync();
                if (model.Attachments.Any())
                {
                    foreach (var attachment in model.Attachments)
                    {
                        ModelAttachment newAttachment = new ModelAttachment()
                        {
                            ModelId = newModel.ModelId,
                            Path = attachment.Path,
                            Type = attachment.Type
                        };
                        await _dbContext.AddAsync(newAttachment);
                    }
                }
                else newModel.ModelAttachments = new List<ModelAttachment>();
                await _dbContext.SaveChangesAsync();
                return Created("Create new model successfully", new
                {
                    ModelId = newModel.UnitId,
                    ProductId = newModel.ProductId,
                    UnitId = newModel.UnitId,
                    ColorId = newModel.ColorId,
                    Specification = newModel.Specification,
                    PrimaryPrice = newModel.PrimaryPrice,
                    SecondaryPrice = newModel.SecondaryPrice,
                    Available = newModel.Available,
                    Description = newModel.Description,
                    Attachments = newModel.ModelAttachments.Select(a => new
                    {
                        Path = a.Path,
                        Type = a.Type
                    })
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response("Error", "An error occurs when create new model"));
            }
        }

        //Post

        //User
    }
}
