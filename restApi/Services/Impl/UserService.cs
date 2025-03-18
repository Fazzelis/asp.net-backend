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

    private string GenerateJwt(User user)
    {
        List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name) };
        var generatedJwt = new JwtSecurityToken(
            issuer: JwtOptions.ISSURE,
            audience: JwtOptions.AUDINCE,
            claims: claims,
            signingCredentials: new SigningCredentials(JwtOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(generatedJwt);
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

        string jwtTokenStr = GenerateJwt(newUser);
        JwtToken db_jwt = new JwtToken { Token = jwtTokenStr, UserId = newUser.Id, ExpirationTime = DateTime.UtcNow.AddMinutes(30) };
        _context.Add(db_jwt);
        _context.SaveChanges();

        Dictionary<string, string> response = new Dictionary<string, string>(){
            {"token", jwtTokenStr}
        };
        return response;
    }

    public string? LogIn(UserLogin userLogin)
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

        var jwtToken = _context.JwtTokens.FirstOrDefault(t => t.UserId == db_user.Id);
        if (jwtToken != null)
        {
            return jwtToken.Token;
        }

        var newJwtToken = GenerateJwt(db_user);
        JwtToken db_newJwtToken = new JwtToken{Token=newJwtToken, UserId=db_user.Id, ExpirationTime=DateTime.UtcNow.AddMinutes(30)};
        _context.Add(db_newJwtToken);
        _context.SaveChanges();

        return newJwtToken;
    }

    public User? LogInWithJwt(string token)
    {
        var jwt = _context.JwtTokens.FirstOrDefault(jwtoken => jwtoken.Token == token);
        if (jwt == null)
        {
            return null;
        }
        if (DateTime.UtcNow > jwt.ExpirationTime)
        {
            _context.JwtTokens.Remove(jwt);
            _context.SaveChanges();
            return null;
        }
        return _context.Users.FirstOrDefault(u => u.Id == jwt.UserId);
    }
}