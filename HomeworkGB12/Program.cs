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

            builder.Configuration.AddJsonFile("ocelot.json", false, true);

            builder.Services.AddOcelot(builder.Configuration);
            builder.Services.AddSwaggerForOcelot(builder.Configuration);

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
