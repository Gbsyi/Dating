using System.Reflection;
using Dating.Api;
using Dating.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<IDatingDbContext, DatingDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("Default"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapApiEndpoints();

app.Run();