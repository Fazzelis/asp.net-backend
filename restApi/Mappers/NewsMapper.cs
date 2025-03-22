using restApi.Dtos.News;
using restApi.Entity;

namespace restApi.Mappers;

public static class NewsMapper
{
    public static News toNews(this NewsPostDto newsDto, string author, Guid authorId)
    {
        return new News{
            Title=newsDto.Title,
            Description=newsDto.Description,
            Author=author,
            PublicTime=DateTime.UtcNow,
            AuthorId=authorId
        };
    }
    public static NewsGetDto toNewsGetDto(this News news)
    {
        return new NewsGetDto{
            Title=news.Title,
            Description=news.Description,
            Author=news.Author
        };
    }
}