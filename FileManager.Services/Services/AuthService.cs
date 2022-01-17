using FileManager.Entities.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace FileManager.Services.Services;

public class AuthService
{    
    public Session CreateSession(User user, string securityKey)
    {
        var expiresInMilSeconds = DateTimeOffset.Now.AddHours(24).ToUnixTimeMilliseconds().ToString();

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),                
                new Claim(ClaimTypes.Email, user.Email)                
            };

        //Symmetric Security Key
        var byteSymmetricSecurityKey = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(securityKey));

        //Credentials
        var credentials = new SigningCredentials(byteSymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        //ExpiresIn
        var expiresIn = DateTime.Now.AddHours(24);

        //Payload
        var payload = new JwtSecurityToken(claims: claims, expires: expiresIn, signingCredentials: credentials, issuer: user.Id.ToString());
        var jwt = new JwtSecurityTokenHandler().WriteToken(payload);

        return new Session(expiresInMilSeconds, jwt);
    }

    public JwtSecurityToken DecodeToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();       
        
        return tokenHandler.ReadJwtToken(token);
    }
}
