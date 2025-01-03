using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Repositories.Implementations;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services.Implementations;
using TodoApi.Services.Interfaces;

namespace TodoApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var config = builder.Configuration;
        

        // Add services to the container.
        services.AddDbContext<TodoApiDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DbConnection")));
        
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<ITodoService, TodoService>();

        services.AddMemoryCache();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddMemoryCache();
        builder.Services.AddInMemoryRateLimiting();
        builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
        builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseIpRateLimiting();
        //app.UseMiddleware<CustomRateLimitMiddleware>();
        
        app.MapControllers();

        app.Run();
    }
}