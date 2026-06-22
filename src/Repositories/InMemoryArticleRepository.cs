using Publishing.Models;

namespace Publishing.Repositories;

public class InMemoryArticleRepository : IArticleRepository
{
    private readonly List<Article> _articles = new()
    {
        new Article { Id = 1, Title = "Onboarding the new design system", AuthorName = "Ana Ruiz", Status = "Published", PublishedAt = DateTime.UtcNow.AddDays(-3) },
        new Article { Id = 2, Title = "Quarterly engineering update", AuthorName = "Marcus Webb", Status = "Published", PublishedAt = DateTime.UtcNow.AddDays(-20) },
        new Article { Id = 3, Title = "A retrospective on our 2025 roadmap", AuthorName = "Priya Nair", Status = "Published", PublishedAt = DateTime.UtcNow.AddDays(-95) },
        new Article { Id = 4, Title = "Draft: migrating the billing service", AuthorName = "Ana Ruiz", Status = "Draft", PublishedAt = null },
    };

    public IReadOnlyList<Article> GetAll()
    {
        return _articles;
    }

    public Article? GetById(int id)
    {
        return _articles.FirstOrDefault(article => article.Id == id);
    }

    public void Update(Article article)
    {
        var index = _articles.FindIndex(existing => existing.Id == article.Id);
        if (index >= 0)
        {
            _articles[index] = article;
        }
    }
}
