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
                        Name = "Все"
                    }
                );
            }
            content.SaveChanges();
            if (!content.Products.Any())
            {
                content.AddRange(
                    new Product
                    {
                        Name = "Night City",
                        Description = "Описание картинки(изменить)",
                        PathImg = "/img/nightcity.png"
                    },
                    new Product
                    {
                        Name = "The Banner Saga 1",
                        Description = "Описаник картинки(изменить)",
                        PathImg = "/img/thebannersaga.jpg"
                    },
                    new Product
                    {
                        Name = "Attack on titan",
                        Description = "Описаник картинки(изменить)",
                        PathImg = "/img/attackontitan.jpg"
                    },
                    new Product
                    {
                        Name = "Gris",
                        Description = "Описаник картинки(изменить)",
                        PathImg = "/img/gris.png"
                    },
                    new Product
                    {
                        Name = "Road",
                        Description = "Описаник картинки(изменить)",
                        PathImg = "/img/road.jpg"
                    }
                );
            }
            content.SaveChanges();
        }
    }
}
