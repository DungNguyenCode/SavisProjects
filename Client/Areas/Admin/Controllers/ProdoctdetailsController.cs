using AspNetCoreHero.ToastNotification.Abstractions;
using Client.ViewModel;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles ="Admin")]
    public class ProductdetailsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyf;
        private readonly List<string> AllApi;
        public Guid _idpro;

        public ProductdetailsController(HttpClient httpClient, INotyfService notyf)
        {
            _httpClient = httpClient;
            _notyf = notyf;
            // Khởi tạo danh sách API 
            AllApi = new List<string>
        {
            "https://localhost:7294/api/ProductDetail/get-all",
            "https://localhost:7294/api/ProductDetail/post",
            "https://localhost:7294/api/ProductDetail/getbyid/",
            "https://localhost:7294/api/ProductDetail/put/",
            "https://localhost:7294/api/ProductDetail/delete/",
        };

        }
        [HttpGet]
        public List<Color> GetColor()
        {
            var response = _httpClient.GetAsync("https://localhost:7294/api/Color/get-all").Result;//Gửi yêu cầu

            var responseData = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Color>>(responseData);
            return data;

        }
        [HttpGet]
        public List<Material> GetMaterial()
        {
            var response = _httpClient.GetAsync("https://localhost:7294/api/Material/get-all").Result;//Gửi yêu cầu

            var responseData = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Material>>(responseData);
            return data;

        }
        [HttpGet]
        public List<Category> GetCategory()
        {
            var response = _httpClient.GetAsync("https://localhost:7294/api/Category/get-all").Result;//Gửi yêu cầu

            var responseData = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Category>>(responseData);
            return data;

        }
        [HttpGet]
        public List<Role> GetRole()
        {
            var response = _httpClient.GetAsync("https://localhost:7294/api/Role/get-all").Result;//Gửi yêu cầu

            var responseData = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Role>>(responseData);
            return data;

        }
        [HttpGet]
        public List<Size> GetSize()
        {
            var response = _httpClient.GetAsync("https://localhost:7294/api/Size/get-all").Result;//Gửi yêu cầu

            var responseData = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Size>>(responseData);
            return data;

        }
        [HttpGet]
        public List<Brand> GetBrand()
        {
            var response = _httpClient.GetAsync("https://localhost:7294/api/Brand/get-all").Result;//Gửi yêu cầu

            var responseData = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Brand>>(responseData);

            return data;


        }
        public List<Image> GetImage()
        {
            var response = _httpClient.GetAsync("https://localhost:7294/api/Image/get-all").Result;//Gửi yêu cầu

            var responseData = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Image>>(responseData);

            return data;


        }        
        public List<Product> GetProduct()
        {
            var response = _httpClient.GetAsync("https://localhost:7294/api/Product/get-all").Result;//Gửi yêu cầu

            var responseData = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Product>>(responseData);

            return data;


        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ViewBag.Color = GetColor();
            ViewBag.Brands = GetBrand();
            ViewBag.Category = GetCategory();
            ViewBag.Size = GetSize();
            ViewBag.Material = GetMaterial();
            ViewBag.Images = GetImage();
            ViewBag.Pros = GetProduct();
            var response = await _httpClient.GetAsync(AllApi[0]);//Gửi yêu cầu
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ProductDetail>>(responseData);
                return View(data);
            }
            else
            {
                _notyf.Error(response.StatusCode.ToString());
                return View();
            }
        }
        public IActionResult Create()
        {
            ViewBag.Color = GetColor();
            ViewBag.Brands = GetBrand();
            ViewBag.Category = GetCategory();
            ViewBag.Size = GetSize();
            ViewBag.Material = GetMaterial();
            return View();
        }
        public async Task<string> AddImg(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                //Trỏ tới thư mục wwwroot để tí copy sang
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productimages", imageFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    //Thực hiện copy ảnh sang thư mục mới wwwroot
                    await imageFile.CopyToAsync(stream);
                }
            }

            return imageFile.FileName;
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductsVm item, IFormFile imageFile)
        {

            var product = new Product()
            {
                Code = item.CodeProduct,
                Id = Guid.NewGuid(),
                Name = item.NameProduct,
                Quantity = item.QuantityProduct,
            };
            var productDetails = new ProductDetail()
            {
                Id = Guid.NewGuid(),
                Id_Brand = item.Id_Brand,
                Id_Category = item.Id_Category,
                Id_Color = item.Id_Color,
                Id_Material = item.Id_Material,
                Id_Size = item.Id_Size,
                Description = item.Description,
                Gender = item.Gender,
                Price = item.Price,
                Status = 1,
                Id_Product = product.Id
            };
            var img = new Image()
            {
                Id = product.Id,
                Status = 1,
                Name = product.Name,
                Id_Product_details = productDetails.Id,

            };
            var jsondataProduct = JsonConvert.SerializeObject(product);
            var contentProduct = new StringContent(jsondataProduct, Encoding.UTF8, "application/json");
            var responeseProduct = await _httpClient.PostAsync("https://localhost:7294/api/Product/post", contentProduct);

            var jsondataProductDetails = JsonConvert.SerializeObject(productDetails);
            var contentProductDetails = new StringContent(jsondataProductDetails, Encoding.UTF8, "application/json");
            var responeseProductDetails = await _httpClient.PostAsync(AllApi[1], contentProductDetails);

            if (imageFile != null && imageFile.Length > 0)
            {

                img.ImageFile = await AddImg(imageFile);

            }

            var jsondataimg = JsonConvert.SerializeObject(img);
            var contentImg = new StringContent(jsondataimg, Encoding.UTF8, "application/json");
            var responeseImg = await _httpClient.PostAsync("https://localhost:7294/api/Image/post", contentImg);


            if (responeseProduct.IsSuccessStatusCode && responeseProductDetails.IsSuccessStatusCode && responeseImg.IsSuccessStatusCode)
            {
                _notyf.Success("Thêm thành công!");
                return Redirect("~/Admin/Productdetails/GetAll");
            }
            _notyf.Error("Lỗi!");
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Color = GetColor();
            ViewBag.Brands = GetBrand();
            ViewBag.Category = GetCategory();
            ViewBag.Size = GetSize();
            ViewBag.Material = GetMaterial();
            ViewBag.Img = GetImage();


            var response = await _httpClient.GetFromJsonAsync<ProductDetail>(AllApi[2] + $"{id}");
            var responsePro = await _httpClient.GetFromJsonAsync<Product>("https://localhost:7294/api/Product/getbyid/" + $"{response.Id_Product}");

            var reponeseImg = await _httpClient.GetFromJsonAsync<Image>("https://localhost:7294/api/Image/getbyid/" + $"{response.Id_Product}");
            if (response != null && responsePro != null)
            {
                var productdetail = new ProductsVm()
                {
                    NameProduct = responsePro.Name,
                    QuantityProduct = responsePro.Quantity,
                    Id_Brand = response.Id_Brand,
                    Id_Category = response.Id_Category,
                    Id_Color = response.Id_Color,
                    Id_Material = response.Id_Material,
                    Id_Size = response.Id_Size,
                    Description = response.Description,
                    Gender = response.Gender,
                    Price = response.Price,
                    Id_Product = response.Id_Product,
                    CodeProduct = responsePro.Code,
                    ImageFile = reponeseImg.ImageFile


                };
                return View(productdetail);
            }

            _notyf.Error("Not Found!");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductsVm item, IFormFile imageFile)
        {
            var productDetails = new ProductDetail()
            {
                Id_Brand = item.Id_Brand,
                Id_Category = item.Id_Category,
                Id_Color = item.Id_Color,
                Id_Material = item.Id_Material,
                Id_Size = item.Id_Size,
                Description = item.Description,
                Gender = item.Gender,
                Price = item.Price,
                Id_Product = item.Id_Product,
                Status = 1,

            };
            var product = new Product()
            {
                Code = item.CodeProduct,
                Id = productDetails.Id_Product,
                Name = item.NameProduct,
                Quantity = item.QuantityProduct,
            };

            var img = new Image()
            {

                Status = 1,
                Name = product.Name,

                Id_Product_details = id,

            };
            if (imageFile != null && imageFile.Length > 0)
            {

                img.ImageFile = await AddImg(imageFile);
            }
            var resultpro = await _httpClient.PutAsJsonAsync<Product>("https://localhost:7294/api/Product/put/" + $"{productDetails.Id_Product}", product);
            var resultimg = await _httpClient.PutAsJsonAsync<Image>("https://localhost:7294/api/Image/put/" + $"{productDetails.Id_Product}", img);
            var result = await _httpClient.PutAsJsonAsync<ProductDetail>(AllApi[3] + $"{id}", productDetails);


            if (result.IsSuccessStatusCode && resultpro.IsSuccessStatusCode && resultimg.IsSuccessStatusCode)
            {
                _notyf.Success("Update success!");
                return Redirect("~/Admin/ProductDetails/GetAll");
            }
            _notyf.Error($"Error: {result.StatusCode.ToString()}"!);
            return View();
        }
        public async Task<IActionResult> Delete(Guid id, ProductsVm item)
        {

            var result = await _httpClient.DeleteAsync(AllApi[4] + $"{id}");
            var resultpro = await _httpClient.DeleteAsync("https://localhost:7294/api/Product/delete/" + $"{item.Id_Product}");
            var resultimg = await _httpClient.DeleteAsync("https://localhost:7294/api/Image/delete/" + $"{item.Id_Product}");

            if (result.IsSuccessStatusCode && resultpro.IsSuccessStatusCode && resultimg.IsSuccessStatusCode)
            {
                _notyf.Success("Delete success!");
                return Redirect("~/Admin/ProductDetails/GetAll");
            }
            _notyf.Error($"Error: {result.StatusCode.ToString()}"!);
            return View();

        }
    }
}
