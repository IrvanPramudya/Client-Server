using API.Contracts;
using API.Data;
using API.Repositories;
using API.Services;
using API.Utilities.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        builder.Services.AddControllers()
                        .ConfigureApiBehaviorOptions(options =>
                        {
                            options.InvalidModelStateResponseFactory = _context =>
                            {
                                var errors = _context.ModelState.Values
                                                     .SelectMany(v => v.Errors)
                                                     .Select(v => v.ErrorMessage);

                                return new BadRequestObjectResult(new ResponseValidationHandler
                                {
                                    Code = StatusCodes.Status400BadRequest,
                                    Status = HttpStatusCode.BadRequest.ToString(),
                                    Message = "Validation Error",
                                    Errors = errors.ToArray()
                                });
                            };
                        });
        // Add services to the container.
        var connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<BookingDbContext>(option => option.UseSqlServer(connection));
        // Add Service Lifetime Repository
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
        builder.Services.AddScoped<IBookingRepository, BookingRepository>();
        builder.Services.AddScoped<IEducationRepository, EducationRepository>();
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();
        builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();

        //Service Lifetime Services
        builder.Services.AddScoped<UniversityService>();
        builder.Services.AddScoped<RoomService>();
        builder.Services.AddScoped<RoleService>();
        builder.Services.AddScoped<EducationService>();
        builder.Services.AddScoped<BookingService>();
        builder.Services.AddScoped<AccountService>();
        builder.Services.AddScoped<AccountRoleService>();
        builder.Services.AddScoped<EmployeeService>();

        // Add SmtpClient to the container.
        builder.Services.AddTransient<IEmailHandler, EmailHandler>(_ => new EmailHandler(
            builder.Configuration["EmailService:SmtpServer"],
            int.Parse(builder.Configuration["EmailService:SmtpPort"]),
            builder.Configuration["EmailService:FromEmailAddress"]
        ));

        //Add Register Token JWT
        builder.Services.AddScoped<ITokenHandler, API.Utilities.Handlers.TokenHandler>();

        //Register Fluent Validation
        builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Metrodata Coding Camp",
                Description = "ASP.NET Core API 6.0"
            });
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
        });


        // CORS Configuration 
        builder.Services.AddCors(
            option =>
            {
                option.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyOrigin();
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                    });
            });
        // Jwt Configuration
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTConfig:SecretKey"])),
                       ValidateIssuer = false,
                       //Usually, this is your application base URL
                       ValidIssuer = builder.Configuration["JWTConfig:UrlServer"],
                       ValidateAudience = false,
                       //If the JWT is created using a web service, then this would be the consumer URL.
                       ValidAudience = builder.Configuration["JWTConfig:UrlClient"],
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero
                   };
               });

        // Build app
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}