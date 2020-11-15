using Mar.Data.Models;
using System.Collections.Generic;

namespace Mar.Data.Interfaces
{
    public interface IProductsCategories
    {
        IEnumerable<Category> GetAllCategories { get; }
        public void AddCategory(string name);
        public void DeleteCategory(int id);
    }
}
