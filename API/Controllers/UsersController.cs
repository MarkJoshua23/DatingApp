using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//all endpoints are now needed authorization
//Can now access User object to get the username in token
[Authorize]
//putting injection inside () is the cleaner version
//inherit the apicontroller form baseapicontroller
//inject the repository that manages the dbcontext
public class UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : BaseApiController
{

    [HttpGet]
    //Task is used for async
    //ActionResult can return http responses
    //IEnumerable is used when returning a collection like lists, etc
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        //use await for possible code that can block the process
        //use repository method
        var users = await userRepository.GetMembersAsync();

        //map users<AppUser> to <MemberDto>
        // var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);

        //status code 200 ok along with the users
        return Ok(users);
    }



    // api/users/{username}
    [HttpGet("{username}")]
    //no IEnumerable if the return is one item
    public async Task<ActionResult<MemberDto>> GetUsers(string username)
    {
        var user = await userRepository.GetMemberAsync(username);

        //null checker
        if (user == null) return NotFound();

        //status code 200 ok along with the users mapped to Dto
        // return mapper.Map<MemberDto>(user);
        return user;
    }


    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var user = await userRepository.GetUserByUserNameAsync(User.GetUsername());
        if (user == null) return BadRequest("Could nto find user");
        //updates the user with the updates values 
        mapper.Map(memberUpdateDto, user);

        //saveallasync will not return true if theres no changes in values
        if (await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
    }

    [HttpPost("add-photo")]
    //receives a file
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        //get the user info in db by username
        var user = await userRepository.GetUserByUserNameAsync(User.GetUsername());
        if (user == null) return BadRequest("Cannot update user");

        //upload
        var result = await photoService.AddPhotoAsync(file);
        //if upload fails
        if(result.Error != null) return BadRequest(result.Error.Message);

        //make a photo object to return
        var photo = new Photo{
            Url = result.SecureUrl.AbsoluteUri,
            PublicId=result.PublicId,
        };

        //add the photo object to the photos list in appuser entity
        user.Photos.Add(photo);

        //return photodto if successfully saved 
        if (await userRepository.SaveAllAsync()) return mapper.Map<PhotoDto>(photo);

        return BadRequest("Problem adding photo ");

    }



}
