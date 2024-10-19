using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ToDoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:5500")  // Your frontend URL
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                  });
            });

            // Add Swagger services.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Use CORS before other middlewares.
            app.UseCors(MyAllowSpecificOrigins);

            // Use Static Files Middleware
            app.UseStaticFiles();  // This serves your static files (HTML, CSS, JS)

            // Use Swagger middleware.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Map controllers (your Web API routes)
            app.MapControllers();

            app.Run();
        }
    }
}
