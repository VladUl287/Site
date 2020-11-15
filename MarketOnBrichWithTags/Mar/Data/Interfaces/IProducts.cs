using Mar.Data.Models;
using System.Collections.Generic;

namespace Mar.Data.Interfaces
{
    public interface IProducts
    {
        IEnumerable<Product> GetAllProducts { get; }
        IEnumerable<Product> GetFavProducts { get; }
        Product GetProductById(int id);
        Product GetProductByName(string name);
        public void AddProduct(Product product);
        public void DeleteProduct(Product product);
        public void EditProduct(int id, string category);
    }
}
