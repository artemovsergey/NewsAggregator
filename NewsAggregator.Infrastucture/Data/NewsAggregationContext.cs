using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using NewsAggregator.Domen.Models;

namespace NewsAggregator.Infrastucture.Data;

public class NewsAggregationContext : DbContext
{
    public NewsAggregationContext(DbContextOptions<NewsAggregationContext> options) : base(options) 
    {
        Database.Migrate();
    }
 
    public NewsAggregationContext(){ }

    static NewsAggregationContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<News> NewsList { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=MirtekNewsAggregation;Username=postgres;Password=postgres");
        //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MirtekNewsAggregation;Username=postgres;Password=root");
    }

}
