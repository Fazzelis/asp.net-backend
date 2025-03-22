using restApi.Dtos.News;
using restApi.Entity;

namespace restApi.Controllers;

public interface INewsService
{
    public Guid? createNews(NewsPostDto newsDto, string jwt);
    public News? getNewsById(Guid newsId);
}