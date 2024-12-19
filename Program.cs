using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebPlantApi.Models;
using System.Text;

namespace WebPlantApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Cấu hình kết nối cơ sở dữ liệu PostgreSQL với EF Core
            builder.Services.AddDbContext<PlantDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PlantDatabase"))
            );

            // Thêm các dịch vụ vào container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Cấu hình Swagger
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebPlant API", Version = "v1" });
            });

            // Cấu hình JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                    };
                });

            // Cấu hình CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Cấu hình pipeline HTTP request.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); // Đảm bảo gọi UseAuthentication trước UseAuthorization
            app.UseAuthorization();

            // Sử dụng CORS với tên policy "AllowAll"
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
