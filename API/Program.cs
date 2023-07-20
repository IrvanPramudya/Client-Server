using API.Contracts;
using API.Data;
using API.Models;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Add services to the container.
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookingDbContext>(option => option.UseSqlServer(connection));

// Add Service Lifetime
builder.Services.AddScoped<ITableRepository<University>, UniversityRepository>();
builder.Services.AddScoped<ITableRepository<Education>, EducationRepository>();
builder.Services.AddScoped<ITableRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<ITableRepository<Room>, RoomRepository>();
builder.Services.AddScoped<ITableRepository<Booking>, BookingRepository>();
builder.Services.AddScoped<ITableRepository<Role>, RoleRepository>();
builder.Services.AddScoped<ITableRepository<AccountRole>, AccountRoleRepository>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
