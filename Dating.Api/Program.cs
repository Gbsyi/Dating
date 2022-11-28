using System.Reflection;
using Dating.Api;
using Dating.Api.Common.Services;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<IDatingDbContext, DatingDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IUserContext, UserContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapApiEndpoints();

app.Run();