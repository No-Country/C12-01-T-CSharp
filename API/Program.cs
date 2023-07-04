using Core.interfaces;
using Infrastructure.repositorios;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Core;
using Infrastructure;
using System.Text.Json.Serialization;
using Core.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
/*builder.Services.AddControllers().AddJsonOptions(x =>
x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());*/
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGenerica<Autor>,GenericoRepo<Autor>>();
builder.Services.AddScoped<IGenerica<Libro>,GenericoRepo<Libro>>();
builder.Services.AddScoped<IGenerica<Genero>,GenericoRepo<Genero>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
