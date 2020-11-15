using Mar.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Mar.Controllers
{
    public class TagController : Controller
    {
        private readonly IProductsTags _productsTags;
        private readonly IProductsRelations _productsRelations;
        public TagController(IProductsTags productsCategory, IProductsRelations productsRelations)
        {
            _productsTags = productsCategory;
            _productsRelations = productsRelations;
        }

        public IActionResult Index() => View(_productsTags.AllTags.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _productsTags.AddTag(name);
                return RedirectToAction("Index");
            }
            return View(name);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var tag = _productsTags.GetTagById(id);
            if (tag != null)
            {
                _productsRelations.DeleteRelation(tag.Id);
                _productsTags.DeleteTag(tag);
            }
            return RedirectToAction("Index");
        }
    }
}
