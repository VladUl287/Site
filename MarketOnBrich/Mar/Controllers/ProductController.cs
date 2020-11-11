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
        public IActionResult Create(string name, string desc, IFormFile file, List<int> category)
        {
            if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(desc) && file == null && category == null))
            {
                string path = "/img/" + file.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                Product product = new Product { Name = name, Description = desc, PathImg = path };
                var a = _products.AddProduct(product);
                var c = _productsCategories.FindListCategories(category);
                for (int i = 0; i < c.Count; i++)
                {
                    Relations relation = new Relations { productId = a.Id, categoryId = c[i].Id };
                    _products.AddRelation(relation);
                }
                return RedirectToAction("Index");
            }
            return View(name, desc);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _products.GetProductById(id);
            if (product != null)
            {
                List<Relations> listRelations = _products.ProductRelations(id);
                List<int> idCategories = new List<int>();
                foreach (var relation in listRelations)
                {
                    idCategories.Add(relation.categoryId);
                }
                var listCategories = _productsCategories.FindListCategories(idCategories);
                var allCategories = _productsCategories.AllCategories.ToList();
                ChangeProductModel model = new ChangeProductModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    AllCategories = allCategories,
                    ProductCategories = listCategories
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int productId, List<int> idCategories, string description)
        {
            var product = await _products.GetProductById(productId);
            if (product != null)
            {
                _products.EditProduct(productId, description);
                List<Relations> relations = new List<Relations>();
                for (int i = 0; i < idCategories.Count; i++)
                {
                    relations.Add(new Relations { productId = productId, categoryId = idCategories[i] });
                }
                _products.ChangeRelation(relations);
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
