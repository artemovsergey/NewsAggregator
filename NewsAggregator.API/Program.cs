using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Interfaces;
using NewsAggregator.Infrastucture.Data;
using NewsAggregator.Infrastucture.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();

string connection = builder.Configuration.GetConnectionString("PostgreSQLDocker");
builder.Services.AddDbContext<NewsAggregationContext>(options => options.UseNpgsql(connection));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
 
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
