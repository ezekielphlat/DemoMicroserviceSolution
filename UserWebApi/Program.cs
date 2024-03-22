using Microsoft.EntityFrameworkCore;

namespace UserWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPass = Environment.GetEnvironmentVariable("DB_POSTGRES_PASSWORD");
            var connectionString = $"Server={dbHost};Port=5432;Database={dbName};Username=postgres;Password={dbPass}";
            builder.Services.AddDbContext<UserDbContext>(opt => opt.UseNpgsql(connectionString));


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}