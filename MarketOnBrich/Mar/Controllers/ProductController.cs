using Mar.Data.Interfaces;
using Mar.Data.Models;
using Mar.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mar.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProducts _products;
        private readonly IProductsCategories _productsCategories;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductController(IProducts products, IProductsCategories productsCategories, IWebHostEnvironment appEnvironment)
        {
            _products = products;
            _productsCategories = productsCategories;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View(_products.AllProducts.ToList());
        }

        public IActionResult Create()
        {
             return View(_productsCategories.AllCategories);
        }

        [HttpPost]
        public IActionResult Create(string name, string desc, IFormFile file, string category)
        {
            if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(desc) && file == null))
            {
                string path = "/img/" + file.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                Product product = new Product { Name = name, Description = desc, PathImg = path, CategoryName = category };
                _products.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(name, desc);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _products.GetProductById(id);
            if (product != null)
            {
                var productCategories = product.CategoryName;
                var allCategories = _productsCategories.AllCategories.ToList();
                ChangeProductModel model = new ChangeProductModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    AllCategories = allCategories,
                    ProductCategory = productCategories
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int productId, string category, string description)
        {
            var product = await _products.GetProductById(productId);
            if (product != null)
            {
                _products.EditProduct(productId, category, description);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Download(int id)
        {
            var product = await _products.GetProductById(id);
            if (product != null)
            {
                var file = new FileInfo(_appEnvironment.WebRootPath + product.PathImg);
                byte[] mas = await System.IO.File.ReadAllBytesAsync(file.FullName);
                return File(mas, "application/" + file.Extension, file.Name);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _products.GetProductById(id);
            if (product != null)
            {
                _products.DeleteProduct(product);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
