using AspNetCoreHero.ToastNotification.Abstractions;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyf;

        public RoleController(HttpClient httpClient, INotyfService notyf)
        {
            _httpClient = httpClient;
            _notyf = notyf;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apiUrl = "https://localhost:7294/api/Role/get-all";
            var response = await _httpClient.GetAsync(apiUrl);//Gửi yêu cầu
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Role>>(responseData);
                _notyf.Success("Thành công!");
                return View(data);
            }
            else
            {
                _notyf.Error(response.StatusCode.ToString());
                return View();
            }
        }
    }
}


