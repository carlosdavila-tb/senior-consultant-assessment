namespace Publishing.Models;

public class Article
{
    public required int Id { get; init; }
    public required string Title { get; set; }
    public required string AuthorName { get; init; }
    public string Status { get; set; } = "Draft";
    public DateTime? PublishedAt { get; set; }
}

public record ArticleView
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required string AuthorName { get; init; }
    public required string Status { get; init; }
    public DateTime? PublishedAt { get; init; }
}
