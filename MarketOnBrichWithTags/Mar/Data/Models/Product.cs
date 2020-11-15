
namespace Mar.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PathImg { get; set; }
        public uint NumOfDownloads { get; set; } = 0;
        public bool IsFavorite { get; set; } = false;
        public string CategoryName { get; set; }
    }
}
