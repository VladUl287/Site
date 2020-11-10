using Mar.Data.Models;
using System.Collections.Generic;

namespace Mar.ViewModels
{
    public class ChangeProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public List<Category> AllCategories { get; set; }
        public string ProductCategory { get; set; }
    }
}
