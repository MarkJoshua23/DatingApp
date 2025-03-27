using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
//inject dbcontext/datacontext
public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    //return only the required values of memberdto
    public async Task<MemberDto?> GetMemberAsync(string username)
    {
        return await context.Users
        .Where(x=> x.UserName == username)
        .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await context.Users
        .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
        .ToListAsync();
    }

    //return users with the given id
    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        //findasync is for id
        return await context.Users.FindAsync(id);
    }

    //return all appuser and photos entity
    public async Task<AppUser?> GetUserByUserNameAsync(string username)
    {
        //get the user with the same username
        return await context.Users
        .Include(x=> x.Photos)
        .SingleOrDefaultAsync(x=> x.UserName == username);
    }

    //get all users
    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await context.Users
        //include the photos tbl, like joins
        .Include(x=> x.Photos)
        .ToListAsync();
    }


    public async Task<bool> SaveAllAsync()
    {
        //return an int(the number of changes)
        //if the changes are > 0 then something changed
        //return true if > 0
        return await context.SaveChangesAsync() > 0 ;
    }

    //tell entity that the entity has been modified
    public void Update(AppUser user)
    {
        context.Entry(user).State =EntityState.Modified;
    }
}
