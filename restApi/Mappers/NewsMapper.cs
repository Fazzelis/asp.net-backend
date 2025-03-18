using restApi.Dtos.News;
using restApi.Models;

namespace restApi.Mappers;

public static class NewsMapper
{
    public static News toNews(NewsDto newsDto, string author)
    {
        return new News{
            Title=newsDto.Title,
            Description=newsDto.Description,
            Author=author,
            PublicTime=DateTime.UtcNow
        };
    }
}