using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace HomeworkGB12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("ocelot.json")
                                .Build();

            builder.Services.AddOcelot(configuration);
            builder.Services.AddSwaggerForOcelot(configuration);

            var app = builder.Build();

            app.UseSwagger();

            app.UseSwaggerForOcelotUI(options =>
            {
                options.PathToSwaggerGenerator = "/swagger/docs";
            }).UseOcelot().Wait();

            app.UseHttpsRedirection();

            app.Run();
        }
    }
}
