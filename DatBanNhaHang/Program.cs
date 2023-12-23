using DatBanNhaHang.Payloads.Converters.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Đặt bàn nhà hàng",
        Version = "v1"
    });

        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            In = ParameterLocation.Header,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Description = "Nhập token vào đây"
        });

        x.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddScoped<IBaiViet,BaiVietServices>();
builder.Services.AddScoped<IBan, BanServices>();
builder.Services.AddScoped<IChiTietHoaDon,ChiTietHoaDonServices>();
builder.Services.AddScoped<IDauBep, DauBepServices>();
builder.Services.AddScoped<IHoaDon, HoaDonServices>();
builder.Services.AddScoped<IKhachHang, KhachHangServices>();
builder.Services.AddScoped<ILienHe, LienHeServices>();
builder.Services.AddScoped<ILoaiBan, LoaiBanServices>();
builder.Services.AddScoped<ILoaiMonAn, LoaiMonAnServices>();
builder.Services.AddScoped<IMonAn, MonAnServices>();
builder.Services.AddScoped<INhanXet, NhanXetServices>();
builder.Services.AddScoped<ITrangThaiHoaDon, TrangThaiHoaDonServices>();
builder.Services.AddScoped<IThongKe, ThongKeServices>();


builder.Services.AddSingleton<ResponseObject<UserDTO>>();
builder.Services.AddSingleton<ResponseObject<TokenDTO>>();
builder.Services.AddSingleton<ResponseObject<AdminDTOs>>();
builder.Services.AddSingleton<ResponseObject<BaiVietDTOs>>();
builder.Services.AddSingleton<ResponseObject<BanDTOs>>();
builder.Services.AddSingleton<ResponseObject<ChiTietHoaDonDTOs>>();
builder.Services.AddSingleton<ResponseObject<DauBepDTOs>>();
builder.Services.AddSingleton<ResponseObject<HoaDonDTO>>();
builder.Services.AddSingleton<ResponseObject<KhachHangDTOs>>();
builder.Services.AddSingleton<ResponseObject<LienHeDTOs>>();
builder.Services.AddSingleton<ResponseObject<LoaiBanDTOs>>();
builder.Services.AddSingleton<ResponseObject<LoaiMonAnDTOs>>();
builder.Services.AddSingleton<ResponseObject<MonAnDTOs>>();
builder.Services.AddSingleton<ResponseObject<NhanXetDTOs>>();
builder.Services.AddSingleton < ResponseObject<NhanXetDTOs>>();
builder.Services.AddSingleton<ResponseObject<TrangThaiHoaDonDTOs>>();
builder.Services.AddSingleton<UserConverters>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("AppSettings:SecretKey").Value!))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
