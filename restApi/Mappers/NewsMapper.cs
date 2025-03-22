using restApi.Dtos.News;
using restApi.Entity;

namespace restApi.Mappers;

public static class NewsMapper
{
    public static News toNews(this NewsPostDto newsDto, User user)
    {
        return new News{
            Title=newsDto.Title,
            Description=newsDto.Description,
            PublicTime=DateTime.UtcNow,
            Author=user
        };
    }
    public static NewsGetDto toNewsGetDto(this News news)
    {
        return new NewsGetDto{
            Title=news.Title,
            Description=news.Description,
            Author=news.Author.Name
        };
    }
}