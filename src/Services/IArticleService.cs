using Publishing.Models;

namespace Publishing.Services;

public interface IArticleService
{
    IReadOnlyList<ArticleView> GetArticles();

    IReadOnlyList<string> GetAuditTrail();

    ArticleView Publish(int id);
}
