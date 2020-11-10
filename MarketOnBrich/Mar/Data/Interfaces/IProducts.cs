using Mar.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mar.Data.Interfaces
{
    public interface IProducts
    {
        IEnumerable<Product> AllProducts { get; }
        IEnumerable<Product> GetFavProducts { get; }
        public void EditProduct(int id, string category, string description);
        public void AddProduct(Product product);
        Task<Product> GetProductById(int id);
        public void DeleteProduct(Product product);
    }
}
