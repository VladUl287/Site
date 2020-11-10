using Mar.Data.Models;
using System.Collections.Generic;

namespace Mar.Data.Interfaces
{
    public interface IProductsCategories
    {
        IEnumerable<Category> AllCategories { get; }
        public Category FindCategoryById(int id);
        public List<Category> FindCategoryById(List<int> categoriesId);
        public void AddCategory(string category);
        public void DeleteCategory(Category category);
    }
}
