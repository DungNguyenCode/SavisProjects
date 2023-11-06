using AspNetCoreHero.ToastNotification.Abstractions;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyf;
        private readonly List<string> AllApi;

        public CategoryController(HttpClient httpClient, INotyfService notyf)
        {
            _httpClient = httpClient;
            _notyf = notyf;
            // Khởi tạo danh sách API 
            AllApi = new List<string>
        {
            "https://localhost:7294/api/Category/get-all",
            "https://localhost:7294/api/Category/post",
            "https://localhost:7294/api/Category/getbyid/",
            "https://localhost:7294/api/Category/put/",
            "https://localhost:7294/api/Category/delete/",
        };

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var response = await _httpClient.GetAsync(AllApi[0]);//Gửi yêu cầu
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Category>>(responseData);
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category item)
        {

            var jsonData = JsonConvert.SerializeObject(item);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responese = await _httpClient.PostAsync(AllApi[1], content);
            if (responese.IsSuccessStatusCode)
            {
                _notyf.Success("Thêm thành công!");
                return Redirect("~/Admin/Category/GetAll");
            }
            _notyf.Error("Lỗi!");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<Category>(AllApi[2] + $"{id}");
            if (response != null)
            {
                return View(response);
            }
            _notyf.Error("Not Found!");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Category item)
        {

            var result = await _httpClient.PutAsJsonAsync<Category>(AllApi[3] + $"{id}", item);
            if (result.IsSuccessStatusCode)
            {
                _notyf.Success("Update success!");
                return Redirect("~/Admin/Category/GetAll");
            }
            _notyf.Error($"Error: {result.StatusCode.ToString()}"!);
            return View();
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _httpClient.DeleteAsync(AllApi[4] + $"{id}");
            if (result.IsSuccessStatusCode)
            {
                _notyf.Success("Delete success!");
               return Redirect("~/Admin/Category/GetAll");
            }
            _notyf.Error($"Error: {result.StatusCode.ToString()}"!);
            return View();

        }
    }
}
