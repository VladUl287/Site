using Mar.Data.Interfaces;
using Mar.Data.Models;
using System.Collections.Generic;

namespace Mar.Data.Repository
{
    public class CategoriesRepository : IProductsCategories
    {
        private readonly ApplicationContext _appDbContent;
        public CategoriesRepository(ApplicationContext appDbContent)
        {
            _appDbContent = appDbContent;
        }
        public IEnumerable<Category> AllCategories => _appDbContent.Categories;

        public Category FindCategoryById(int id)
        {
            return _appDbContent.Categories.Find(id);
        }

        public List<Category> FindListCategories(List<int> listId)
        {
            List<Category> categories = new List<Category>();
            for (int i = 0; i < listId.Count; i++)
            {
                categories.Add(_appDbContent.Categories.Find(listId[i]));
            }
            return categories;
        }

        public void AddCategory(string name)
        {
            var category = new Category() { Name = name };
            _appDbContent.Categories.Add(category);
            _appDbContent.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _appDbContent.Remove(category);
            _appDbContent.SaveChanges();
        }
    }
}
