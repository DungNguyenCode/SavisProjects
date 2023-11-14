using Client.Models;
using Client.ViewModel;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;


        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;

        }

        [HttpGet]
        public async Task<IActionResult> Index(CombinedViewModel item)
        {
            var responseImg = await _httpClient.GetAsync("https://localhost:7294/api/Image/get-all");
            var responsePro = await _httpClient.GetAsync("https://localhost:7294/api/Product/get-all");
            var responseProDetail = await _httpClient.GetAsync("https://localhost:7294/api/ProductDetail/get-all");

            if (responseImg.IsSuccessStatusCode && responsePro.IsSuccessStatusCode && responseProDetail.IsSuccessStatusCode)
            {
                var productData = JsonConvert.DeserializeObject<List<Product>>(await responsePro.Content.ReadAsStringAsync());
                var productDetailData = JsonConvert.DeserializeObject<List<ProductDetail>>(await responseProDetail.Content.ReadAsStringAsync());
                var imageData = JsonConvert.DeserializeObject<List<Image>>(await responseImg.Content.ReadAsStringAsync());

                if (productData == null || imageData == null || productDetailData == null)
                {
                    return View();
                }
                var combinedData = new CombinedViewModel
                {
                    Products = productData,
                    ProductDetails = productDetailData,
                    Images = imageData
                };
                return View(combinedData);
            }

            return View();
        }

        [HttpGet]
        public async Task<string> GetBrand(Guid id_brand)
        {
            var response = await _httpClient.GetAsync("https://localhost:7294/api/Brand/getbyid/" + $"{id_brand}");//Gửi yêu cầu
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Brand>(responseData);
                return data.Name;
            }
            return "No Data";

        }
        [HttpGet]
        public async Task<string> GetSize(Guid id_Size)
        {
            var response = await _httpClient.GetAsync("https://localhost:7294/api/Size/getbyid/" + $"{id_Size}");//Gửi yêu cầu
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Size>(responseData);
                return data.Name;
            }
            return "No Data";

        }
        [HttpGet]
        public async Task<string> GetColor(Guid id_color)
        {
            var response = await _httpClient.GetAsync("https://localhost:7294/api/Color/getbyid/" + $"{id_color}");//Gửi yêu cầu
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Color>(responseData);
                return data.Name;
            }
            return "No Data";

        }
        [HttpGet]
        public async Task<string> GetCategory(Guid id_cate)
        {
            var response = await _httpClient.GetAsync("https://localhost:7294/api/Category/getbyid/" + $"{id_cate}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Category>(responseData);
                return data.Name;
            }
            return "No Data";

        }
        [HttpGet]
        public async Task<string> GetMaterial(Guid id_mate)
        {
            var response = await _httpClient.GetAsync("https://localhost:7294/api/Material/getbyid/" + $"{id_mate}");//Gửi yêu cầu
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Material>(responseData);
                return data.Name;
            }
            return "No Data";

        }
        public async Task<string> GetProduct(Guid id_product)
        {
            var response = await _httpClient.GetAsync("https://localhost:7294/api/Product/getbyid/" + $"{id_product}");//Gửi yêu cầu
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Product>(responseData);
                return data.Name;
            }
            return "No Data";

        }
        public async Task<string> GetImg(Guid id_img)
        {
            var response = await _httpClient.GetAsync("https://localhost:7294/api/Image/getbyid/" + $"{id_img}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Image>(responseData);
                return data.ImageFile;
            }
            return "No Data";

        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(Guid id)
        {

            var responseProDetail =  _httpClient.GetAsync("https://localhost:7294/api/ProductDetail/getbyid/" + $"{id}").Result;
            if (responseProDetail.IsSuccessStatusCode)
            {
                var responseData = await responseProDetail.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ProductDetail>(responseData);
                var pro = new ProductsVm()
                {
                    BrandName = GetBrand(data.Id_Brand).Result,
                    ColorName = GetColor(data.Id_Color).Result,
                    MaterialName = GetMaterial(data.Id_Material).Result,
                    CategoryName = GetCategory(data.Id_Category).Result,
                    NameProduct = GetProduct(data.Id_Product).Result,
                    ImageFile = GetImg(data.Id_Product).Result,
                    SizeName = GetSize(data.Id_Size).Result,
                    Description= data.Description,
                    Price=data.Price,
                };



                return View(pro);

            }
            return View(Error());

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}