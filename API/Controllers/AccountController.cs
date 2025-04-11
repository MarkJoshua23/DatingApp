using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//inject dbcontext
//inject itokenservice interface that uses the token service method 
public class AccountController(DataContext context, ITokenService tokenService, IMapper mapper) : BaseApiController
{
    //the route will now be api/account/register
    [HttpPost("register")]
    //return type should be userdto
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {

        //check if username already exist
        if (await UserExists(registerDto.Username)) return BadRequest("Username is already taken");


        //using handles the disposing after using
        using var hmac = new HMACSHA512();

        //put values in the AppUser Entity
        var user = mapper.Map<AppUser>(registerDto);
        //save username in lowercase
        user.UserName = registerDto.Username.ToLower();
        //turn the string password to bytes then hash it
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        //store the salt generated when hashing
        user.PasswordSalt = hmac.Key;



        //dbcontext => Dbset Users => Sqlite Add => insert users
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return new UserDto
        {
            Username = user.UserName,
            //use the createtoken method inside the interface
            Token = tokenService.CreateToken(user),
            KnownAs = user.KnownAs,
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        //firstordefault looks for user and if theres none it returns null
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        //if user does not exist
        if (user == null) return Unauthorized("Invalid Username");

        //if user exist, pass the key/salt to hmac
        using var hmac = new HMACSHA512(user.PasswordSalt);

        //hash the password from user logging in using the salt from existing user
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        //loop as long as the i is smaller than the length
        for (int i = 0; i < computedHash.Length; i++)
        {
            //compare each byte array values to see if theres difference
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        return new UserDto
        {
            Username = user.UserName,
            //use the createtoken method inside the interface
            Token = tokenService.CreateToken(user),
            KnownAs = user.KnownAs
        };
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
