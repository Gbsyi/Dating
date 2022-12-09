using System.Reflection;
using Dating.Api;
using Dating.Api.Common.Services;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Конфигурация Сервисов
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<IDatingDbContext, DatingDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IUserContext, UserContext>();

// Настройка Middleware
var app = builder.Build();

app.UseCors("DefaultPolicy");
// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapApiEndpoints();
app.MapControllers();

app.Run();