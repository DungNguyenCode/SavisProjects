using AspNetCoreHero.ToastNotification.Abstractions;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

using PagedList.Core.Mvc;
using PagedList.Core;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyf;
        private readonly List<string> AllApi;

        public ColorController(HttpClient httpClient, INotyfService notyf)
        {
            _httpClient = httpClient;
            _notyf = notyf;
            // Khởi tạo danh sách API 
            AllApi = new List<string>
        {
            "https://localhost:7294/api/Color/get-all",
            "https://localhost:7294/api/Color/post",
            "https://localhost:7294/api/Color/getbyid/",
            "https://localhost:7294/api/Color/put/",
            "https://localhost:7294/api/Color/delete/",
        };

        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? page)
        {
            if (page == null) page = 1;

            var response = await _httpClient.GetAsync(AllApi[0]);//Gửi yêu cầu
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Color>>(responseData);

                // Tạo một IQueryable từ danh sách
                IQueryable<Color> queryableData = data.AsQueryable();

                // Sử dụng PagedList để phân trang dữ liệu
                int pageSize = 10; // Số mục trên mỗi trang
                int pageNumber = (page ?? 1); // Trang hiện tại
                var model = queryableData.OrderByDescending(c => c.Name).ToPagedList(pageNumber, pageSize);

                return View(model);
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
        public async Task<IActionResult> Create(Color item)
        {

            var jsonData = JsonConvert.SerializeObject(item);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responese = await _httpClient.PostAsync(AllApi[1], content);
            if (responese.IsSuccessStatusCode)
            {
                _notyf.Success("Thêm thành công!");
                return RedirectToAction("GetAll");
            }
            _notyf.Error("Lỗi!");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<Color>(AllApi[2] + $"{id}");
            if (response != null)
            {
                return View(response);
            }
            _notyf.Error("Not Found!");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Color item)
        {

            var result = await _httpClient.PutAsJsonAsync<Color>(AllApi[3] + $"{id}", item);
            if (result.IsSuccessStatusCode)
            {
                _notyf.Success("Update success!");
                return RedirectToAction("GetAll");
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
                return RedirectToAction("GetAll");
            }
            _notyf.Error($"Error: {result.StatusCode.ToString()}"!);
            return View();

        }
    }
}
