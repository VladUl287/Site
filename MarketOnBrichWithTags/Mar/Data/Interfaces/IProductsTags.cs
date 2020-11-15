using Mar.Data.Models;
using System.Collections.Generic;

namespace Mar.Data.Interfaces
{
    public interface IProductsTags
    {
        IEnumerable<Tag> AllTags { get; }
        public Tag GetTagById(int id);
        public List<Tag> GetTagsByNames(string[] listTagsNames);
        public List<Tag> GetTagsByNames(List<string> listTagsNames);
        public void AddTag(string tagName);
        public void AddTags(string[] tagNames);
        public void AddTags(List<string> listTagsNames);
        public void DeleteTag(Tag tag);
    }
}
