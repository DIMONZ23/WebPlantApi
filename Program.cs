using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebPlantApi.Models;

namespace WebPlantApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Thêm các dịch vụ vào container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Cấu hình Swagger
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebPlant API", Version = "v1" });
            });

            var app = builder.Build();

            // Cấu hình pipeline HTTP request.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
