using BusinessLayer.DTOs;
using BusinessLayer.Services;
using BusinessLayer.Services.Abstraction;
using BusinessLayer.Validator;
using DataAccessLayer.Configuration;
using DataAccessLayer.Data;
using DataAccessLayer.DataAccessRepositories;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using FirebaseAdmin;
using FluentValidation.AspNetCore;
using Google.Apis.Auth.OAuth2;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Text;


namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IManagerServices, ManagerServices>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ISupportService, SupportService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            builder.Services.AddScoped<IEmailService, EmailSender>();
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddControllers();

            // NLog: Setup NLog for Dependency injection
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();


            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("private_key.json"),
            });


            //builder.services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
            //});




            builder.Services.AddDbContext<DataContext>(options =>

               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //   builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(RolesConstent.SupportManager, policy => policy.RequireRole(RolesConstent.SupportManager));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; // Set to true in production
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4a2f77b72f4c0a2e4f62f8e49c1b9a3622a91cfdd20c70a3bf3deeeae2b2f3a60124d492b92c9ebdcb1f6db1c3a9c9f2d5dfb08303e3915010f6d3dbbfa202b3")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = "your_issuer",
                    ValidAudience = "your_audience",
                };
            });





            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: \"Bearer abc123\"",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] {}
            }
        });
            });


            builder.Services.AddControllers().AddFluentValidation(fValid =>
              fValid.RegisterValidatorsFromAssemblyContaining<UserLoginValidator>());

            builder.Services.AddControllers().AddFluentValidation(fValid =>
              fValid.RegisterValidatorsFromAssemblyContaining<UserRegisterValidator>());


            //  builder.Services.AddHealthChecks();
            builder.Services.AddHealthChecks()
                   .AddDbContextCheck<DataContext>()
                   .AddSqlServer(
                      connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                      healthQuery: "SELECT 1;",
                      name: "SQL Check",
                      failureStatus: HealthStatus.Unhealthy);


           


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapHealthChecks("health", new HealthCheckOptions
            {

                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
            
        }
    }
}
