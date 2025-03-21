using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;



//putting database injection inside () is the cleaner version
//inherit the apicontroller form baseapicontroller
public class UsersController(DataContext context) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    //Task is used for async
    //ActionResult can return http responses
    //IEnumerable is used when returning a collection like lists, etc
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        //use await for possible code that can block the process
        var users = await context.Users.ToListAsync();

        //status code 200 ok along with the users
        // return Ok(users);

        return users;
    }


    [Authorize]
    // api/users/{id}
    [HttpGet("{id}")]
    //no IEnumerable if the return is one item
    public async Task<ActionResult<AppUser>> GetUsers(int id)
    {
        var user = await context.Users.FindAsync(id);

        //null checker
        if (user == null) return NotFound();

        //status code 200 ok along with the users
        return user;
    }



}
