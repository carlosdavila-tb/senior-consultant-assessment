using Microsoft.AspNetCore.Mvc;
using Publishing.Models;
using Publishing.Services;

namespace Publishing.Controllers;

[ApiController]
[Route("articles")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<ArticleView>> GetAll()
    {
        return Ok(_articleService.GetArticles());
    }

    [HttpGet("audit")]
    public ActionResult<IReadOnlyList<string>> GetAudit()
    {
        return Ok(_articleService.GetAuditTrail());
    }

    [HttpPost("{id:int}/publish")]
    public ActionResult<ArticleView> Publish(int id)
    {
        try
        {
            var view = _articleService.Publish(id);
            return Ok(view);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
