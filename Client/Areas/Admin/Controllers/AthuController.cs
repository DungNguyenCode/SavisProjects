using API.ModelView;
using AspNetCoreHero.ToastNotification.Abstractions;
using Client.Extensions;
using Client.ViewModel;
using Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using LoginV = Client.ViewModel.LoginV;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AthuController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyf;
        private readonly List<string> AllApi;
        private readonly IConfiguration _configuration;

        public AthuController(HttpClient httpClient, INotyfService notyf, IConfiguration configuration)
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
                "https://localhost:7294/api/User/login",
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
                return Redirect("~/Admin/Athu/Login");

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

        public async Task<IActionResult> Login(LoginV item)
        {
            if (string.IsNullOrEmpty(item.Email) || string.IsNullOrEmpty(item.Password))
            {
                _notyf.Error("Vui lòng nhập email và mật khẩu.");
                return RedirectToAction("Login", "Athu");
            }

            var md5pass = MD5Pass.GetMd5Hash(item.Password);
            item.Password = md5pass;
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(AllApi[5], content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var loginResult = JsonConvert.DeserializeObject<TokenV>(responseData);
                TokenV tokenV = new TokenV { AccessToken = loginResult.AccessToken };
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(loginResult.AccessToken);

                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, item.Email), // Thêm thông tin khác của người dùng nếu cần

                        };

                // Trích xuất thông tin quyền từ mã thông báo JWT
                var roles = jwt.Claims.ToList();
                bool checkRoleAdmin = false;

                // Thêm các quyền từ mã thông báo JWT vào danh tính của người dùng
                if (roles.Any())
                {
                    foreach (var role in roles)
                    {
                        if (role.Type.ToString() == "role")
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.Value));
                            checkRoleAdmin = true;

                            break;
                        }

                    }
                    if (checkRoleAdmin == false)
                    {
                        // Nếu không có quyền từ mã thông báo JWT, thêm quyền mặc định "Customer"
                        claims.Add(new Claim(ClaimTypes.Role, "Customer"));
                    }

                }
                var customData = jwt.Claims.FirstOrDefault(c => c.Type == "Avatar")?.Value;
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                Response.Cookies.Append("AccessToken", loginResult.RefreshToken);
                if (checkRoleAdmin == true)
                {
                    _notyf.Success($"Login success! Welcome {item.Email}");
                    return Redirect("~/Admin/Home/Index");
                    
                }
                else
                {
                    
                    return Redirect("~/Home/Index");
                    
                }

                
               
            }

            _notyf.Error($"Error: {response.StatusCode.ToString()}!");
            return BadRequest("Đăng nhập thất bại");

        }

        public async Task<IActionResult> Signout()
        {
            Response.Cookies.Delete("AccessToken");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/Admin/Athu/Login");
        }

    }
}
