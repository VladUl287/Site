using Mar.Data.Interfaces;
using Mar.Data.Models;
using Mar.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Web.Helpers;

namespace Mar.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProducts _products;
        private readonly IProductsCategories _productsCategories;
        private readonly IProductsTags _productsTags;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IProductsRelations _productsRelations;

        public ProductController(IProducts products, IProductsCategories productsCategories,
            IWebHostEnvironment appEnvironment, IProductsTags productsTags, IProductsRelations productsRelations)
        {
            _products = products;
            _productsCategories = productsCategories;
            _appEnvironment = appEnvironment;
            _productsTags = productsTags;
            _productsRelations = productsRelations;
        }

        public IActionResult Index() => View(_products.GetAllProducts);

        public IActionResult Create() => View(_productsCategories.GetAllCategories);

        public IActionResult Main(int id)
        {
            var product = _products.GetProductById(id);
            if (product != null)
            {
                var file = new FileInfo(_appEnvironment.WebRootPath + product.PathImg);
                byte[] mas = System.IO.File.ReadAllBytes(file.FullName);
                WebImage photo = new WebImage(mas);
                MainProductModel mainProduct = new MainProductModel
                {
                    Product = product,
                    ImageInfo = photo
                };
                return View(mainProduct);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(string productName, IFormFile image, string category, string tags)
        {
            string path = "/img/" + image.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            Product product = new Product { Name = productName, PathImg = path, CategoryName = category };
            string[] addTags = tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            _productsTags.AddTags(addTags);
            _products.AddProduct(product);
            var listTags = _productsTags.GetTagsByNames(addTags);
            var productRel = _products.GetProductByName(productName);
            _productsRelations.AddRelations(productRel.Id, listTags);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = _products.GetProductById(id);
            if (product != null)
            {
                var allProductCategories = _productsCategories.GetAllCategories;
                var allProductRelations = _productsRelations.GetProductRelations(id);
                List<Tag> tags = new List<Tag>();
                foreach (var item in allProductRelations)
                {
                    tags.Add(_productsTags.GetTagById(item.tagId));
                }
                ChangeProductModel model = new ChangeProductModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    AllCategories = allProductCategories,
                    ProductCategory = product.CategoryName,
                    ProductTags = tags
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(int productId, string category, string newtags)
        {
            var product = _products.GetProductById(productId);
            if (product != null)
            {
                _products.EditProduct(productId, category);
                if (newtags != "")
                {
                    string[] newTags = newtags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    _productsTags.AddTags(newTags);
                    var listTags = _productsTags.GetTagsByNames(newTags);
                    _productsRelations.AddRelations(productId, listTags);
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Download(int id)
        {
            var product = _products.GetProductById(id);
            if (product != null)
            {
                var file = new FileInfo(_appEnvironment.WebRootPath + product.PathImg);
                byte[] mas = System.IO.File.ReadAllBytes(file.FullName);
                return File(mas, "application/" + file.Extension, file.Name);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _products.GetProductById(id);
            if (product != null)
            {
                _products.DeleteProduct(product);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
