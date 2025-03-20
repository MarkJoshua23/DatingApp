using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//inject dbcontext
public class AccountController(DataContext context) : BaseApiController
{
    //the route will now be api/account/register
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {

        //check if username already exist
        if (await UserExists(registerDto.Username)) return BadRequest("Username is already taken");


        //using handles the disposing after using
        using var hmac = new HMACSHA512();

        //put values in the AppUser Entity
        var user = new AppUser
        {
            //save username in lowercase
            UserName = registerDto.Username.ToLower(),
            //turn the string password to bytes then hash it
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            //store the salt generated when hashing
            PasswordSalt = hmac.Key
        };
        //dbcontext => Dbset Users => Sqlite Add => insert users
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    //private method that will only be used in this class
    //will return bool
    //Task is used since its async
    private async Task<bool> UserExists(string username)
    {
        //check if theres existing username
        //to lower to make the case the same
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
