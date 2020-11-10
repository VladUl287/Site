using Mar.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Mar.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductsCategories _productsCategory;

        public CategoryController(IProductsCategories productsCategory)
        {
            _productsCategory = productsCategory;
        }
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View(_productsCategory.AllCategories.ToList());
        }
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _productsCategory.AddCategory(name);
                return RedirectToAction("Index");
            }
            return View(name);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var category = _productsCategory.FindCategoryById(id);
            if (category != null)
            {
                _productsCategory.DeleteCategory(category);
            }
            return RedirectToAction("Index");
        }
    }
}
