using HomeworkGB12.Abstractions;
using HomeworkGB12.DatabaseModel;
using HomeworkGB12.Mapper;
using HomeworkGB12.Repo;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

namespace HomeworkGB12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("api_auth", new OpenApiInfo { Title = "API Authenticate", Version = "v1" });

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Token",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        { 
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddAuthentication().AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

            string connectionString = builder.Configuration.GetConnectionString("AuthenticateDb")!;

            builder.Services.AddScoped(x => new AuthenticateDbContext(connectionString));
            builder.Services.AddScoped<IAuthenticateDbContext, AuthenticateDbContext>(x => new AuthenticateDbContext(connectionString));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();

            builder.Configuration.AddJsonFile("ocelot.json", false, true);

            builder.Services.AddOcelot(builder.Configuration);
            builder.Services.AddSwaggerForOcelot(builder.Configuration);

            var app = builder.Build();

            app.UseSwagger();

            app.UseDeveloperExceptionPage();

            app.UseSwaggerForOcelotUI().UseOcelot().Wait();
            /*options =>
            {
                options.PathToSwaggerGenerator = "/swagger/docs";
            }*/

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
