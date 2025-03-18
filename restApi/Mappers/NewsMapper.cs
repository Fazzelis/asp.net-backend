using restApi.Dtos.News;
using restApi.Models;

namespace restApi.Mappers;

public static class NewsMapper
{
    public static News toNews(NewsPostDto newsDto, string author)
    {
        return new News{
            Title=newsDto.Title,
            Description=newsDto.Description,
            Author=author,
            PublicTime=DateTime.UtcNow
        };
    }
    public static NewsGetDto toNewsGetDto(News news)
    {
        return new NewsGetDto{
            Title=news.Title,
            Description=news.Description,
            Author=news.Author
        };
    }
}