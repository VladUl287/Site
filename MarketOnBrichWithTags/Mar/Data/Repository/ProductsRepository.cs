using Mar.Data.Interfaces;
using Mar.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mar.Data.Repository
{
    public class ProductsRepository : IProducts
    {
        private readonly ApplicationContext _appDbContent;
        public ProductsRepository(ApplicationContext appDbContent)
        {
            _appDbContent = appDbContent;
        }
        public IEnumerable<Product> GetAllProducts => _appDbContent.Products;

        public IEnumerable<Product> GetFavProducts => _appDbContent.Products.Where(c => c.IsFavorite == true);

        public Product GetProductById(int id)
        {
            return _appDbContent.Products.Find(id);
        }

        public Product GetProductByName(string name)
        {
            return _appDbContent.Products.First(c => c.Name == name);
        }

        public void AddProduct(Product product)
        {
            _appDbContent.Products.Add(product);
            _appDbContent.SaveChanges();
        }
        public void DeleteProduct(Product product)
        {
            _appDbContent.Products.Remove(product);
            _appDbContent.SaveChanges();
        }

        public async void EditProduct(int id, string category)
        {
            var product = await _appDbContent.Products.FindAsync(id);
            product.CategoryName = category;
            _appDbContent.SaveChanges();
        }
    }
}
