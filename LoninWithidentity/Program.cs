using Data.ContextDbSavis;
using Data.Interface;
using Data.Models;
using Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ContextDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IAllinterface<User>, UserService>();
builder.Services.AddScoped<IAllinterface<Brand>, BrandService>();
builder.Services.AddScoped<IAllinterface<Role>, RoleService>();
builder.Services.AddScoped<IAllinterface<Size>, SizeService>();
builder.Services.AddScoped<IAllinterface<Product>, ProductService>();
builder.Services.AddScoped<IAllinterface<Image>, ImageService>();
builder.Services.AddScoped<IAllinterface<Material>, MaterialService>();
builder.Services.AddScoped<IAllinterface<ProductDetail>, ProductDetailService>();
builder.Services.AddScoped<IAllinterface<Order>, OrderService>();
builder.Services.AddScoped<IAllinterface<OrderDetails>, OrderDetailService>();
builder.Services.AddScoped<IAllinterface<VorcherDetail>, VorcherDetalService>();
builder.Services.AddScoped<IAllinterface<Vorcher>, VorcherService>();
builder.Services.AddScoped<IAllinterface<Accounts>, AccountService>();
builder.Services.AddScoped<IAllinterface<Cart>, CartServices>();
builder.Services.AddScoped<IAllinterface<CartDetails>, CartDetailService>();
builder.Services.AddScoped<IAllinterface<Category>, CategoryService>();
builder.Services.AddScoped<IAllinterface<PaymentMethod>, PaymenMethodService>();
builder.Services.AddScoped<IAllinterface<Address>, AddressService>();


var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //Thêm cái này vào để không bị chặn khi call API
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
