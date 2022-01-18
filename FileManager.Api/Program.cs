using FileManage.DataAccess.Data;
using FileManage.DataAccess.DbAccess;
using FileManage.DataAccess.Interfaces;
using FileManager.Api.Extensions;
using FileManager.Api.Helpers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Swagger support for bearer header
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization hedader using the bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


//Dependencies Injection
builder.Services.DependenciesContainerExtension();

//Authentication Shceme
builder.Services.AddJwtBearerAuthorizationSchema(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    builder.Configuration["connectionId"] = "dev";
}

if (!app.Environment.IsDevelopment())
{
    builder.Configuration["connectionId"] = "prod";
}

app.UseStaticFiles();

app.UseHttpsRedirection();

//Cors to allow only calls from the angular client
app.UseCors("Cors");

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();

