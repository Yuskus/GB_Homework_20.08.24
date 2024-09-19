
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace HomeworkGB13 //opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway API", Version = "v1" });
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Configuration
                   .AddJsonFile("ocelot.json", false, true)
                   .AddEnvironmentVariables();

            builder.Services.AddOcelot(builder.Configuration);
            builder.Services.AddSwaggerForOcelot(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthorization();

            app.UseSwaggerForOcelotUI(options =>
            {
                options.PathToSwaggerGenerator = "/swagger/docs";
            }).UseOcelot().Wait();

            app.Run();
        }
    }
}
