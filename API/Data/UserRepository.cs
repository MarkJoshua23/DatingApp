using System;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
//inject dbcontext/datacontext
public class UserRepository(DataContext context) : IUserRepository
{
    //return users with the given id
    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        //findasync is for id
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUserNameAsync(string username)
    {
        //get the user with the same username
        return await context.Users.SingleOrDefaultAsync(x=> x.UserName == username);
    }

    //get all users
    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
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
