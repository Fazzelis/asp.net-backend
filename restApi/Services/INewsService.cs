using restApi.Dtos.News;

namespace restApi.Controllers;

public interface INewsService
{
    public Guid? createNews(NewsDto newsDto, string jwt);
}