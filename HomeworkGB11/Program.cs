using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel;
using HomeworkGB11.Queries;
using HomeworkGB11.Repo;

namespace HomeworkGB11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile)); //?????????????

            string connectionString = builder.Configuration.GetConnectionString("EmployeesDb")!;
            builder.Services.AddScoped<IEmployeesDbContext, EmployeesDbContext>(x => new EmployeesDbContext(connectionString));

            builder.Services.AddGraphQLServer().AddQueryType<Query>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapGraphQL();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
