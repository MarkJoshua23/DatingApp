using System;
using API.Entities;

namespace API.Interfaces;

public interface ITokenService
{
    //this is the return 'string'
    string CreateToken(AppUser user)
}
