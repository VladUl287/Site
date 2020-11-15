using Mar.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mar.Data
{
    public class DbInitilizer
    {
        public static void Initial(ApplicationContext content)
        {
            if (!content.Categories.Any())
            {
                content.AddRange(
                    new Category
                    {
                        Name = "Изображения"
                    },
                    new Category
                    {
                        Name = "Anime"
                    },
                    new Category
                    {
                        Name = "Природа"
                    }
                );
            }
            if (!content.Tags.Any())
            {
                content.AddRange(
                    new Tag
                    {
                        Name = "Изображение"
                    }
                );
            }
            if (!content.Relations.Any())
            {
                content.AddRange(
                    new Relations
                    {
                        productId = 1,
                        tagId = 1,
                    },
                    new Relations
                    {
                        productId = 2,
                        tagId = 1,
                    },
                    new Relations
                    {
                        productId = 3,
                        tagId = 1,
                    }
                    );
            }
            content.SaveChanges();
            if (!content.Products.Any())
            {
                content.AddRange(
                    new Product
                    {
                        PathImg = "/img/nightcity.png",
                        CategoryName = content.Categories.FirstOrDefault().Name
                    },
                    new Product
                    {
                        PathImg = "/img/thebannersaga.jpg",
                        CategoryName = content.Categories.FirstOrDefault().Name
                    },
                    new Product
                    {
                        PathImg = "/img/attackontitan.jpg",
                        CategoryName = content.Categories.FirstOrDefault().Name
                    }
                );
            }
            content.SaveChanges();
        }
    }
}
