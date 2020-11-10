using Mar.Data.Interfaces;
using Mar.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mar.Data.Repository
{
    public class ProductsRepository : IProducts
    {
        private readonly ApplicationContext _appDbContent;
        public ProductsRepository(ApplicationContext appDbContent)
        {
            _appDbContent = appDbContent;
        }
        public IEnumerable<Product> AllProducts => _appDbContent.Products;
        public IEnumerable<Product> GetFavProducts => _appDbContent.Products.Where(c => c.IsFavorite == true);

        public void AddProduct(Product product)
        {
            _appDbContent.Products.Add(product);
            _appDbContent.SaveChanges();
        }

        public async void EditProduct(int id, string category, string description)
        {
            var product = await _appDbContent.Products.FindAsync(id);
            product.CategoryName = category;
            product.Description = description;
            _appDbContent.SaveChanges();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _appDbContent.Products.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void DeleteProduct(Product product)
        {
            _appDbContent.Products.Remove(product);
            _appDbContent.SaveChanges();
        }
    }
}
