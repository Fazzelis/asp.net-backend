using Microsoft.AspNetCore.Mvc;
using restApi.Dtos.News;
using restApi.Mappers;

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
    public IActionResult postNews([FromBody] NewsPostDto newsDto, [FromHeader] string jwt)
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

    [HttpGet("news")]
    public IActionResult getNews([FromQuery] Guid newsId)
    {
        var news = _newsService.getNewsById(newsId);
        if (news == null)
        {
            return BadRequest("News not found");
        }

        return Ok(NewsMapper.toNewsGetDto(news));
    }
}