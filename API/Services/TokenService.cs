using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

//lightbulb => implement itokenservice
public class TokenService(IConfiguration config) : ITokenService
{
    //method required by the ITokenService interface
    public string CreateToken(AppUser user)
    {
        //?? means what will happen if null
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access token key");
        
        if (tokenKey.Length<64) throw new Exception("Your token key needs to be longer");
        
        //same key to encrypt and decrypt
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        //insert claims(username, etc)
        var claims = new List<Claim>{
            //insert username to the claims
            new(ClaimTypes.NameIdentifier, user.UserName),
        };

        //use the key we made based on 64+ length tokenkey
        //what to use in signing/encrypting token
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor{
            //put the claims we made
            Subject = new ClaimsIdentity(claims),
            //token will not be valid after 7 days
            Expires = DateTime.UtcNow.AddDays(7),
            //how token should be encrypted/signed
            SigningCredentials = creds
        };

        //create instance of tokenhandler to use it
        var tokenHandler = new JwtSecurityTokenHandler();

        //use tokenhadler that needs the descriptor
        //returns the jwt
        var token = tokenHandler.CreateToken(tokenDescriptor);

        //writetoken makes the jwt to string
        return tokenHandler.WriteToken(token);
    }
}
