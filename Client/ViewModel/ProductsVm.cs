namespace Client.ViewModel
{
    public class ProductsVm
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime Last_modified_date { get; set; }
        public int Status { get; set; }
        public string NameImg { get; set; }
        public int StatusImg { get; set; }
        public string ImageFile { get; set; }
    }
}
