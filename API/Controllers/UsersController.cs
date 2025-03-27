using System;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//all endpoints are now needed authorization
[Authorize]
//putting injection inside () is the cleaner version
//inherit the apicontroller form baseapicontroller
//inject the repository that manages the dbcontext
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{

    [HttpGet]
    //Task is used for async
    //ActionResult can return http responses
    //IEnumerable is used when returning a collection like lists, etc
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        //use await for possible code that can block the process
        //use repository method
        var users = await userRepository.GetUsersAsync();

        //map users<AppUser> to <MemberDto>
        var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);

        //status code 200 ok along with the users
        return Ok(usersToReturn);
    }



    // api/users/{username}
    [HttpGet("{username}")]
    //no IEnumerable if the return is one item
    public async Task<ActionResult<MemberDto>> GetUsers(string username)
    {
        var user = await userRepository.GetUserByUserNameAsync(username);

        //null checker
        if (user == null) return NotFound();

        //status code 200 ok along with the users mapped to Dto
        return mapper.Map<MemberDto>(user);
    }



}
