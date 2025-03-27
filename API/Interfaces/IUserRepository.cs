using System;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    //return a bool if something changed
    Task<bool> SaveAllAsync();
    //convention to use Async at the end of task name
    //IEnumerable for list
    Task <IEnumerable<AppUser>> GetUsersAsync();
    // ? meaning it can return null
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUserNameAsync(string username);
}
