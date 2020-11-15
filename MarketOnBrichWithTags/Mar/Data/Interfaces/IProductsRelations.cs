using Mar.Data.Models;
using System.Collections.Generic;

namespace Mar.Data.Interfaces
{
    public interface IProductsRelations
    {
        public void AddRelations(int productId, List<Tag> tags);
        public List<Relations> GetProductRelations(int id);
        public void DeleteRelation(int id);
    }
}
