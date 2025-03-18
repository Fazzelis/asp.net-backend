using restApi.Controllers;
using restApi.db;
using restApi.Dtos.News;
using restApi.Mappers;
using restApi.Models;

namespace restApi.Services.Impl;
public class NewsService : INewsService
{
    private readonly ApiDbContext _context;
    public NewsService(ApiDbContext context)
    {
        _context = context;
    }
    public Guid? createNews(NewsDto newsDto, string jwt)
    {
        var jwtToken = _context.JwtTokens.FirstOrDefault(token => token.Token == jwt);
        if (jwtToken == null)
        {
            return null;
        }

        var author = _context.Users.FirstOrDefault(user => user.Id == jwtToken.UserId);
        if (author == null)
        {
            return null;
        }

        if (author.Role == "admin" || author.Role == "writter")
        {
            News db_news = NewsMapper.toNews(newsDto, author.Name);
            _context.Add(db_news);
            _context.SaveChanges();
            return db_news.Id;
        }
        else
        {
            return null;
        }

    }
}