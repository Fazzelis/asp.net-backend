using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using restApi.AuthOptions;
using restApi.db;
using restApi.Dtos.User;
using restApi.Models;

namespace restApi.Services.Impl;

public class UserService : IUserService
{
    private readonly ApiDbContext _context;
    public UserService(ApiDbContext context)
    {
        _context = context;
    }
    public Dictionary<string, string>? CreateUser(User newUser)
    {

        if (string.IsNullOrEmpty(newUser.Email))
        {
            return null;
        }

        string emailPatter = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(emailPatter);

        if (!regex.IsMatch(newUser.Email))
        {
            return null;
        }

        var user = _context.Users.FirstOrDefault(u => u.Email == newUser.Email);
        if (user != null)
        {
            return null;
        }
        _context.Add(newUser);
        _context.SaveChanges();

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, newUser.Name) };

        var jwtToken = new JwtSecurityToken(
            issuer: JwtOptions.ISSURE,
            audience: JwtOptions.AUDINCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
            signingCredentials: new SigningCredentials(JwtOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        string jwtTokenStr = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        JwtToken db_jwt = new JwtToken { Token = jwtTokenStr, UserId = newUser.Id, ExpirationTime = DateTime.UtcNow.AddMinutes(30) };
        _context.Add(db_jwt);
        _context.SaveChanges();

        Dictionary<string, string> response = new Dictionary<string, string>(){
            {"id", jwtTokenStr}
        };
        return response;
    }

    public Guid? LogIn(UserLogin userLogin)
    {
        var db_user = _context.Users.FirstOrDefault(u => u.Email == userLogin.Email);
        if (db_user == null)
        {
            return null;
        }
        if (db_user.Password != userLogin.Password)
        {
            return null;
        }
        return db_user.Id;
    }

    public User? LogInWithJwt(string token)
    {
        var jwt = _context.JwtTokens.FirstOrDefault(jwtoken => jwtoken.Token == token);
        if (jwt == null || DateTime.UtcNow > jwt.ExpirationTime)
        {
            return null;
        }
        return _context.Users.FirstOrDefault(u => u.Id == jwt.UserId);
    }
}