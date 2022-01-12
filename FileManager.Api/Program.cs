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


//Data Access
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();

//Services
builder.Services.AddScoped(typeof(UserService));
builder.Services.AddScoped(typeof(FolderService));
builder.Services.AddScoped(typeof(AuthService));
builder.Services.AddAutoMapper(typeof(MappingProifle));

//Authentication Shceme
builder.Services.AddJwtBearerAuthorizationSchema(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();

