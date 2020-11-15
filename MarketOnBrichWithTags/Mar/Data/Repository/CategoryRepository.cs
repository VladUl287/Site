using Mar.Data.Interfaces;
using Mar.Data.Models;
using System.Collections.Generic;

namespace Mar.Data.Repository
{
    public class CategoryRepository : IProductsCategories
    {
        private readonly ApplicationContext _appDbContent;
        public CategoryRepository(ApplicationContext appDbContent)
        {
            _appDbContent = appDbContent;
        }

        public IEnumerable<Category> GetAllCategories => _appDbContent.Categories;

        public void AddCategory(string name)
        {
            _appDbContent.Categories.Add(new Category { Name = name });
            _appDbContent.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            Category category = _appDbContent.Categories.Find(id);
            _appDbContent.Categories.Remove(category);
            _appDbContent.SaveChanges();
        }
    }
}
