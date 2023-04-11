using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using Todo.Service.Config;
using Todo.Service.Model.User;

namespace Todo.Service.Services;

public class JwtTokenService 
{
    private JwtConfig _config;
    public JwtTokenService( IOptions<JwtConfig> options) {
        _config = options.Value;
    }

    public String Generate(UserItem userIten) {
         var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(_config.Secret));
        var tokenDescriptor = new JwtSecurityToken(
            _config.Issuer,
            _config.Audience,
            new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userIten.Id.ToString()),
                new Claim(ClaimTypes.Name, userIten.Name),
                new Claim(ClaimTypes.Email, userIten.Email),
                new Claim(ClaimTypes.Role, userIten.Role),
            },
            expires: DateTime.UtcNow.AddMinutes(_config.ExpirationInMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
