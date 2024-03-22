using MassTransit;
using OrderWebApi.Models;

namespace OrderWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddMassTransit(config =>
            {
                // elided...
                config.SetKebabCaseEndpointNameFormatter();

                config.UsingRabbitMq((context, cfg) =>
                {
                    var rmqHost = Environment.GetEnvironmentVariable("RMQ_HOST");
                    var rmqDefaultHost = Environment.GetEnvironmentVariable("RMQ_DEFAULT_HOST");
                    var rmqUser = Environment.GetEnvironmentVariable("RMQ_USER");
                    var rmqPass = Environment.GetEnvironmentVariable("RMQ_PASS");
                    //cfg.Host(rmqHost, rmqDefaultHost, h =>
                    //{
                    //    h.Username(rmqUser);
                    //    h.Password(rmqPass);
                    //});

                    cfg.Host($"amqp://{rmqUser}:{rmqPass}@{rmqHost}:5672/{rmqDefaultHost}");


                    cfg.ConfigureEndpoints(context);
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