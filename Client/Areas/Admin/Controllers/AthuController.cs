using AspNetCoreHero.ToastNotification.Abstractions;
using Client.Extensions;
using Client.ViewModel;
using Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AthuController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyf;
        private readonly List<string> AllApi;
        private readonly IConfiguration _configuration;

        public AthuController(HttpClient httpClient, INotyfService notyf,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _notyf = notyf;
            _configuration = configuration;

            // Khởi tạo danh sách API 
            AllApi = new List<string>
            {
                "https://localhost:7294/api/User/get-all",
                "https://localhost:7294/api/User/post",
                "https://localhost:7294/api/User/getbyid/",
                "https://localhost:7294/api/User/put/",
                "https://localhost:7294/api/User/delete/",
            };

        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM item)
        {
            if (string.IsNullOrEmpty(item.Fullname) || string.IsNullOrEmpty(item.PhoneNumber) || string.IsNullOrEmpty(item.ConfirmPassword))
            {
                // Thông báo lỗi cho người dùng
                _notyf.Error("Vui lòng điền đầy đủ thông tin.");
                return RedirectToAction("Register", "Home");
            }

            if (item.Password != item.ConfirmPassword)
            {
                // Thông báo lỗi cho người dùng
                _notyf.Error("Mật khẩu xác nhận không khớp.");
                return RedirectToAction("Register", "Home");
            }

            // Thực hiện đăng ký người dùng và xử lý lỗi hoặc thành công từ API
            var md5pass = MD5Pass.GetMd5Hash(item.Password);
            
            item.Password = md5pass;
            var jsonData = JsonConvert.SerializeObject(item);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responese = await _httpClient.PostAsync(AllApi[1], content);
            if (responese.IsSuccessStatusCode)
            {
                // Đăng nhập người dùng sau khi đăng ký thành công (nếu cần)
                // Xử lý hành động sau khi đăng ký thành công (chẳng hạn chuyển hướng đến trang chính)
                _notyf.Success("Đăng ký tài khoản thành công!");
                return Redirect("/Admin");

            }
            else
            {
                // Xử lý lỗi trả về từ API (nếu có)
                var errorResponse = await responese.Content.ReadAsStringAsync();
                _notyf.Error("Đăng ký thất bại: " + errorResponse);
                return View();
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginVm item)
        {
            if (string.IsNullOrEmpty(item.Email) || string.IsNullOrEmpty(item.Password))
            {
                _notyf.Error("Vui lòng nhập email và mật khẩu.");
                return RedirectToAction("Login", "Athu");
            }

            var md5pass = MD5Pass.GetMd5Hash(item.Password);
            var result = await _httpClient.GetAsync(AllApi[0]);

            if (result.IsSuccessStatusCode)
            {
                var jsonData = await result.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<User>>(jsonData);
                var temp = data.FirstOrDefault(c => c.Email == item.Email && c.Password == md5pass);

                if (temp != null)
                {
                    var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, temp.Email),
                new Claim(ClaimTypes.Name, temp.Fullname),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Avatar", temp.Avatar),
                new Claim("PhoneNumber", temp.PhoneNumber)
            };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var athenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(authClaims),
                        Expires = DateTime.UtcNow.AddHours(2),
                        SigningCredentials = new SigningCredentials(athenKey, SecurityAlgorithms.HmacSha256Signature),
                        Issuer = _configuration["JWT:Issuer"],
                        Audience = _configuration["JWT:Audience"]
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwt = tokenHandler.WriteToken(token);                  

                    _notyf.Success($"Login succses! Welcome {temp.Fullname}");
                    return Redirect("/Admin");
                }
            }

            _notyf.Error($"Error: {result.StatusCode.ToString()}!");
            return BadRequest("Đăng nhập thất bại");
        }


    }
}
