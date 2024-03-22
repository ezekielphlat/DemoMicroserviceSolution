using InventoryWebApi.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace InventoryWebApi
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
            builder.Services.AddDbContext<InventoryDbContext>(opt => opt.UseNpgsql(connectionString));

            builder.Services.AddMassTransit(config =>
            {
                // elided...
                config.SetKebabCaseEndpointNameFormatter();
                config.AddConsumer<OrderConsumer>();
                config.UsingRabbitMq((context, cfg) =>
                {
                    var rmqHost = Environment.GetEnvironmentVariable("RMQ_HOST");
                    var rmqDefaultHost = Environment.GetEnvironmentVariable("RMQ_DEFAULT_HOST");
                    var rmqUser = Environment.GetEnvironmentVariable("RMQ_USER");
                    var rmqPass = Environment.GetEnvironmentVariable("RMQ_PASS");
                    cfg.Host(rmqHost, rmqDefaultHost, h =>
                    {
                        h.Username(rmqUser);
                        h.Password(rmqPass);
                    });

                   // cfg.Host($"amqp://{rmqUser}:{rmqPass}@{rmqHost}:5672/{rmqDefaultHost}");


                    cfg.ConfigureEndpoints(context);
                    //cfg.ReceiveEndpoint("orderQueue", c =>
                    //{
                    //    c.ConfigureConsumer<OrderConsumer>(context);
                    //});
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}