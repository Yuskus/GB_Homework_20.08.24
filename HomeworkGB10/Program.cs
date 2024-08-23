using Autofac;
using Autofac.Extensions.DependencyInjection;
using HomeworkGB10.Abstractions;
using HomeworkGB10.Repo;
using Microsoft.Extensions.FileProviders;

namespace HomeworkGB10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add configuration.
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterInstance(configuration);
                containerBuilder.RegisterType<ProductRepository>()
                                .As<IProductRepository>()
                                .SingleInstance();
                containerBuilder.RegisterType<CategoryRepository>()
                                .As<ICategoryRepository>()
                                .SingleInstance();
                containerBuilder.RegisterType<StorageRepository>()
                                .As<IStorageRepository>()
                                .SingleInstance();
            });

            builder.Services.AddMemoryCache(x => x.TrackStatistics = true);

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            string staticFilePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
            Directory.CreateDirectory(staticFilePath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilePath),
                RequestPath = "/static"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
