using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace restApi.AuthOptions;

public class JwtOptions
{
    public const string ISSURE = "restApi";
    public const string AUDINCE = "restApiUsers";
    const string KEY = "mysupersecret_secretsecretsecretkey!123";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}