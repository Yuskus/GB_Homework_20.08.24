using HomeworkGB11.Abstractions;
using HomeworkGB11.DatabaseModel;
using HomeworkGB11.Queries;
using HomeworkGB11.Mapper;
using HomeworkGB11.Services;
using HomeworkGB11.Mutations;
using Microsoft.EntityFrameworkCore;

namespace HomeworkGB11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            string connectionString = builder.Configuration.GetConnectionString("EmployeesDb")!;

            builder.Services.AddScoped(x => new EmployeesDbContext(connectionString));
            builder.Services.AddScoped<IEmployeesDbContext, EmployeesDbContext>(x => new EmployeesDbContext(connectionString));

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IPositionService, PositionService>();
            builder.Services.AddScoped<IWorkZoneService, WorkZoneService>();

            builder.Services
                   .AddGraphQLServer()
                   .AddQueryType<Query>()
                   .AddMutationType<Mutation>();

            var app = builder.Build();

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
