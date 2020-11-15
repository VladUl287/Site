using Mar.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mar.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductsCategories _productsCategories;
        public CategoryController(IProductsCategories productsCategories)
        {
            _productsCategories = productsCategories;
        }

        public IActionResult Index()
        {
            return View(_productsCategories.GetAllCategories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _productsCategories.AddCategory(name);
                return RedirectToAction("Index");
            }
            return View(name);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _productsCategories.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}
