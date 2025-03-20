using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//inject dbcontext
public class AccountController(DataContext context) : BaseApiController
{
    //the route will now be api/account/register
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(string username, string password)
    {
        //using handles the disposing after using
        using var hmac = new HMACSHA512();

        //put values in the AppUser Entity
        var user = new AppUser
        {
            UserName = username,
            //turn the string password to bytes then hash it
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            //store the salt generated when hashing
            PasswordSalt = hmac.Key
        };
        //dbcontext => Dbset Users => Sqlite Add => insert users
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }
}
