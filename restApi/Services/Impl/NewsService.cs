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
    public Guid? createNews(NewsPostDto newsDto, string jwt)
    {
        var jwtToken = _context.JwtTokens.FirstOrDefault(token => token.Token == jwt);
        if (jwtToken == null)
        {
            return null;
        }

        if (DateTime.UtcNow > jwtToken.ExpirationTime)
        {
            _context.JwtTokens.Remove(jwtToken);
            _context.SaveChanges();
            return null;
        }

        var author = _context.Users.FirstOrDefault(user => user.Id == jwtToken.UserId);
        if (author == null)
        {
            return null;
        }

        if (author.Role == "admin" || author.Role == "writer")
        {
            News db_news = newsDto.toNews(author.Name, author.Id);
            _context.Add(db_news);
            _context.SaveChanges();
            return db_news.Id;
        }
        else
        {
            return null;
        }
    }
    public News? getNewsById(Guid newsId)
    {
        var news = _context.News.FirstOrDefault(n => n.Id == newsId);
        if (news == null)
        {
            return null;
        }
        return news;
    }
}