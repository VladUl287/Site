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

        public Product AddProduct(Product product)
        {
            _appDbContent.Products.Add(product);
            _appDbContent.SaveChanges();
            return _appDbContent.Products.FirstOrDefault(x => x.Name == product.Name);
        }

        public async void EditProduct(int id, string description)
        {
            var product = await _appDbContent.Products.FindAsync(id);
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
        //Relations
        public void AddRelation(Relations relations)
        {
            _appDbContent.Relations.Add(relations);
            _appDbContent.SaveChanges();
        }

        public List<Relations> ProductRelations(int id)
        {
            return _appDbContent.Relations.Where(x => x.productId == id).ToList();
        }

        public void ChangeRelation(List<Relations> relations)
        {
            var a = _appDbContent.Relations.Where(x => x.productId == relations.First().productId);
            if (relations != a)
            {
                _appDbContent.Relations.RemoveRange(a);
                _appDbContent.Relations.AddRange(relations);
                _appDbContent.SaveChanges();
            }
        }
    }
}
