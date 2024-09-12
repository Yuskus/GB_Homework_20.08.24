using Autofac;
using Autofac.Extensions.DependencyInjection;
using HomeworkGB10.Abstractions;
using HomeworkGB10.DatabaseModel;
using HomeworkGB10.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

namespace HomeworkGB10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = WebAppBuilding(args);

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

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static WebApplication WebAppBuilding(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json");

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            string connectionString = builder.Configuration.GetConnectionString("StoreDb")!;

            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.Register(x => new StorageDbContext(connectionString))
                                .AsSelf()
                                .InstancePerLifetimeScope();
                containerBuilder.Register(x => new StorageDbContext(connectionString))
                                .As<IStorageDbContext>()
                                .InstancePerLifetimeScope();
                containerBuilder.RegisterType<ProductRepository>()
                                .As<IProductRepository>()
                                .InstancePerLifetimeScope();
                containerBuilder.RegisterType<CategoryRepository>()
                                .As<ICategoryRepository>()
                                .InstancePerLifetimeScope();
                containerBuilder.RegisterType<StorageShelfRepository>()
                                .As<IStorageShelfRepository>()
                                .InstancePerLifetimeScope();
            });

            builder.Services.AddMemoryCache(x => x.TrackStatistics = true);

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder.Build();
        }
    }
}
