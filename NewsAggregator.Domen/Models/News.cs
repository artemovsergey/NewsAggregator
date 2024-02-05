
namespace NewsAggregator.Domen.Models;

public class News
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DatePublic { get; set; }
    public string? Link { get; set; }
    public string? Hash { get; set; }
  
}
