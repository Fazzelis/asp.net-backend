using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using restApi.db;
using restApi.Models;

namespace restApi.Services.Impl;

public class UserService : IUserService
{
    private readonly ApiDbContext _context;
    // private readonly IPasswordHasher<User> _passwordHasher;
    public UserService(ApiDbContext context)
    {
        _context = context;
        // _passwordHasher = passwordHasher;
    }
    public Dictionary<string, Guid>? CreateUser(User newUser){
        
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
        // newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);
        _context.Add(newUser);
        _context.SaveChanges();
        Dictionary<string, Guid> response = new Dictionary<string, Guid>(){
            {"id", newUser.Id}
        };
        return response;
    }
}