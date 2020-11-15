using Mar.Data.Interfaces;
using Mar.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mar.Data.Repository
{
    public class RelationRepository : IProductsRelations
    {
        private readonly ApplicationContext _appDbContent;
        public RelationRepository(ApplicationContext appDbContent)
        {
            _appDbContent = appDbContent;
        }

        public void AddRelations(int productId, List<Tag> tags)
        {
            foreach (var tag in tags)
            {
                if (_appDbContent.Relations.FirstOrDefault(x => x.tagId == tag.Id) == default)
                {
                    _appDbContent.Relations.Add(new Relations { productId = productId, tagId = tag.Id });
                }
            }
            _appDbContent.SaveChanges();
        }

        public List<Relations> GetProductRelations(int id)
        {
            return _appDbContent.Relations.Where(x => x.productId == id).ToList();
        }

        public void DeleteRelation(int id)
        {
            var relation = _appDbContent.Relations.First(x => x.tagId == id);
            _appDbContent.Relations.Remove(relation);
            _appDbContent.SaveChanges();
        }
    }
}
