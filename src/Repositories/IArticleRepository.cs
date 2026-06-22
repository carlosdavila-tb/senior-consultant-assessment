using Publishing.Models;

namespace Publishing.Repositories;

public interface IArticleRepository
{
    IReadOnlyList<Article> GetAll();

    Article? GetById(int id);

    void Update(Article article);
}
