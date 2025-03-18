using restApi.Dtos.User;
using restApi.Models;

namespace restApi.Services;

public interface IUserService
{
    public Dictionary<string, string>? CreateUser(User newUser);
    public string? LogIn(UserLogin userLogin);
    public User? LogInWithJwt(string token);
    public bool giveUser(string token, string email);
    public bool giveWritter(string token, string email);
    public bool giveAdmin(string token, string email);
}