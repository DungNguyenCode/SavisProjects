using Data.ContextDbSavis;
using Data.Interface;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddDbContext<ContextDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IAllinterface<User>, UserService>();
builder.Services.AddScoped<IAllinterface<Brand>, BrandService>();
builder.Services.AddTransient<IAllinterface<Role>, RoleService>();
builder.Services.AddScoped<IAllinterface<Size>, SizeService>();
builder.Services.AddScoped<IAllinterface<Product>, ProductService>();
builder.Services.AddScoped<IAllinterface<Image>, ImageService>();
builder.Services.AddScoped<IAllinterface<Material>, MaterialService>();
builder.Services.AddScoped<IAllinterface<ProductDetail>, ProductDetailService>();
builder.Services.AddScoped<IAllinterface<Order>, OrderService>();
builder.Services.AddScoped<IAllinterface<OrderDetails>, OrderDetailService>();
builder.Services.AddScoped<IAllinterface<VorcherDetail>, VorcherDetalService>();
builder.Services.AddScoped<IAllinterface<Vorcher>, VorcherService>();
builder.Services.AddScoped<IAllinterface<Cart>, CartServices>();
builder.Services.AddScoped<IAllinterface<Color>, ColorService>();
builder.Services.AddScoped<IAllinterface<CartDetails>, CartDetailService>();
builder.Services.AddScoped<IAllinterface<Category>, CategoryService>();
builder.Services.AddScoped<IAllinterface<PaymentMethod>, PaymenMethodService>();
builder.Services.AddScoped<IAllinterface<Address>, AddressService>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
   

}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
          //tự cấp token
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),    

        //ký vào token
        ValidateIssuerSigningKey = true,

        ClockSkew = TimeSpan.Zero
    };
    
});

//builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
//    policy =>
//    {
//        policy.WithOrigins("https://localhost:7294").AllowAnyMethod().AllowAnyHeader();
//    }));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //Thêm cái này vào để không bị chặn khi call API

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
