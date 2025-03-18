using Microsoft.AspNetCore.Mvc;
using restApi.Dtos.News;

namespace restApi.Controllers;

[Route("api/news")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }
    [HttpPost("create")]
    public IActionResult postNews([FromBody] NewsDto newsDto, [FromHeader] string jwt)
    {
        var newsId = _newsService.createNews(newsDto, jwt);
        if (newsId == null)
        {
            return Conflict("Conflic");
        }
        else
        {
            return Ok(newsId);
        }
    }
}