using Publishing.Models;
using Publishing.Repositories;

namespace Publishing.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _repository;
    private readonly List<string> _auditTrail = [];
    private readonly List<string> _subscriberNotifications = [];
    private readonly List<string> _searchIndex = [];
    private readonly List<Article> _syndicationFeed = [];

    public ArticleService(IArticleRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<ArticleView> GetArticles()
    {
        return _repository.GetAll()
            .Select(article => new ArticleView
            {
                Id = article.Id,
                Title = article.Title,
                AuthorName = article.AuthorName,
                Status = article.Status,
                PublishedAt = article.PublishedAt
            })
            .ToList();
    }

    public IReadOnlyList<string> GetAuditTrail()
    {
        return _auditTrail;
    }

    public ArticleView Publish(int id)
    {
        var article = _repository.GetById(id);

        if (article is null)
        {
            throw new InvalidOperationException($"Article {id} was not found.");
        }

        if (string.IsNullOrWhiteSpace(article.Title))
        {
            throw new InvalidOperationException("An article cannot be published without a title.");
        }

        if (article.Status == "Published")
        {
            throw new InvalidOperationException($"Article {id} is already published.");
        }

        article.Status = "Published";
        article.PublishedAt = DateTime.UtcNow;
        _repository.Update(article);

        _syndicationFeed.Add(article);

        _auditTrail.Add($"{DateTime.UtcNow:o} | article {article.Id} published by {article.AuthorName}");

        _subscriberNotifications.Add($"Subscribers notified: \"{article.Title}\" is now live.");

        _searchIndex.Add($"INDEX article {article.Id}: {article.Title} ({article.AuthorName})");

        return new ArticleView
        {
            Id = article.Id,
            Title = article.Title,
            AuthorName = article.AuthorName,
            Status = article.Status,
            PublishedAt = article.PublishedAt
        };
    }
}
