using System.ComponentModel.DataAnnotations.Schema;

namespace Client.ViewModel
{
    public class ProductsVm
    {
        public Guid Id_Size { get; set; }
        public Guid Id_Category { get; set; }
        public Guid Id_Color { get; set; }
        public Guid Id_Brand { get; set; }
        public Guid Id_Material { get; set; }
        public Guid Id_Product { get; set; }

        public string? BrandName { get; set; }
        public string? ColorName { get; set; }
        public string? MaterialName { get; set; }
        public string? CategoryName { get; set; }
        public string? SizeName { get; set; }

        public string? CodeProduct { get; set; }
        public string? ImageFile { get; set; }
        public string? NameProduct { get; set; }
        public int QuantityProduct { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }
        public int Gender { get; set; }
    }
}
