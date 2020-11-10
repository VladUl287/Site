using System.Collections;
using System.Collections.Generic;

namespace Mar.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathImg { get; set; }
        public uint NumOfDownloads { get; set; } = 0;
        public bool IsFavorite { get; set; } = false;
        public string CategoryName { get; set; }
    }
}
