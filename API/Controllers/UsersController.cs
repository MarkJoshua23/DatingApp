using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[ApiController]
// api/users
[Route("api/[controller]")]
//controllerbase because its mvc without view so it returns json instead of html

//putting database injection inside () is the cleaner version
public class UsersController(DataContext context) : ControllerBase
{

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
