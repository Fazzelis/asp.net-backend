using restApi.Models;

namespace restApi.Services;

public interface IUserService
{
    public Dictionary<string, Guid>? CreateUser(User newUser);
}