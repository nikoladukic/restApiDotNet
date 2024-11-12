using RestApiTemplate.BussinesLogic.Interface;
using RestApiTemplate.BussinesLogic;
using RestApiTemplate.Database;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RestApiTemplate.Database.SqlQuery;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IRestApiTemplateBussinesLogic, RestApiTemplateBussinesLogic>();
builder.Services.AddTransient<SqlQueryBuilder>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// dodavanje connection stringa i povezivvanje sa bazom
builder.Services.AddDbContext<RestApiTemplateDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("RestApiTemplateConnectionString")));

builder.Services.AddAutoMapper(typeof(RestApiTemplate.Models.Mapper.AutoMapper));
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
