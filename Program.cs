using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using web2projekat.Data;
using AutoMapper;
using web2projekat.Mapping;
using web2projekat.Services;
using web2projekat.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<web2projekatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("web2projekatContext") ?? throw new InvalidOperationException("Connection string 'web2projekatContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("cors", build =>
{
         build.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
}));

builder.Services.AddScoped<IKorisnikService, KorisnikService>();
builder.Services.AddScoped<IArtikalService, ArtikalService>();
builder.Services.AddScoped<INarudzbinaService, NarudzbinaService>();
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapiranjeProfila());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("cors");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
