using Data.Models;

namespace Client.ViewModel
{
    public class CombinedViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
        public List<Image> Images { get; set; }
    }

}
