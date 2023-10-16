using System.Text;
using System.Threading.Tasks.Dataflow;
using API.Data;
using API.Entities;
using API.Extension;
using API.Interfaces;
using API.MiddleWare;
using API.Services;
using API.Utils.ConfigOptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>();
builder.Services.Configure<GCSConfigOptions>(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleWare>();

app.UseCors(build => build.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.UseCors(build => build.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7169"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
