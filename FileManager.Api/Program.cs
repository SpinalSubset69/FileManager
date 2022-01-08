using FileManage.DataAccess.Data;
using FileManage.DataAccess.DbAccess;
using FileManage.DataAccess.Interfaces;
using FileManager.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Data Access
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();

//Services
builder.Services.AddScoped(typeof(UserService));
builder.Services.AddScoped(typeof(FolderService));
builder.Services.AddAutoMapper(typeof(MappingProifle));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

//ENDPOINTS

////User endpoints
//app.UserEndpoints();

////Folder endpoints
//app.FolderEndpoints();

////Download endpoints
//app.DownloadEndpoints();

//ENDPONTS

app.UseAuthorization();
app.MapControllers();

app.Run();

