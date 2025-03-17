using Npgsql.Replication;
using restApi.Dtos.User;
using restApi.Models;

namespace restApi.Mappers;

public static class UserMapper
{
    public static User ToUserModel(this UserDto userDto)
    {
        return new User{
            Name=userDto.Name,
            Email=userDto.Email,
            Password=userDto.Password,
            Role="user"
        };
    }

    public static UserInfoDto ToUserInfoDto(this User user)
    {
        return new UserInfoDto{
            Email=user.Email,
            Name=user.Name
        };
    }
}