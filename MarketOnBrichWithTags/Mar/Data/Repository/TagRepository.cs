using Mar.Data.Interfaces;
using Mar.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mar.Data.Repository
{
    public class TagRepository : IProductsTags
    {
        private readonly ApplicationContext _appDbContent;
        public TagRepository(ApplicationContext appDbContent)
        {
            _appDbContent = appDbContent;
        }

        public IEnumerable<Tag> AllTags => _appDbContent.Tags;

        public Tag GetTagById(int id)
        {
            return _appDbContent.Tags.Find(id);
        }

        public List<Tag> GetTagsByNames(string[] listTagsNames)
        {
            List<Tag> tags = new List<Tag>();
            foreach (var tagName in listTagsNames)
            {
                tags.Add(_appDbContent.Tags.FirstOrDefault(x => x.Name == tagName.Trim()));
            }
            return tags;
        }
        public List<Tag> GetTagsByNames(List<string> listTagsNames)
        {
            List<Tag> tags = new List<Tag>();
            foreach (var tagName in listTagsNames)
            {
                tags.Add(_appDbContent.Tags.FirstOrDefault(x => x.Name == tagName));
            }
            return tags;
        }

        public void AddTag(string tagName)
        {
            if (_appDbContent.Tags.FirstOrDefault(x => x.Name == tagName) == default)
            {
                _appDbContent.Tags.Add(new Tag() { Name = tagName });
                _appDbContent.SaveChanges();
            }
        }

        public void AddTags(string[] tagsNames)
        {
            List<string> tags = tagsNames.ToList();
            foreach (var name in tags)
            {
                if (_appDbContent.Tags.FirstOrDefault(x => x.Name == name) == default)
                {
                    _appDbContent.Tags.Add(new Tag() { Name = name.Trim() });
                }
            }
            _appDbContent.SaveChanges();
        }

        public void AddTags(List<string> listTagsNames)
        {
            List<Tag> tags = new List<Tag>();
            foreach (var tagName in listTagsNames)
            {
                if (_appDbContent.Tags.First(x => x.Name == tagName) == default)
                {
                    tags.Add(new Tag { Name = tagName });
                }
            }
            _appDbContent.Tags.AddRange(tags);
            _appDbContent.SaveChanges();
        }

        public void DeleteTag(Tag tag)
        {
            _appDbContent.Remove(tag);
            _appDbContent.SaveChanges();
        }
    }
}
