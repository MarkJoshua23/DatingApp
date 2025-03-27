using System;
using API.DTOs;
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
    //returns all fields of appuser and photos entities: not optimized but more flexible
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUserNameAsync(string username);

    //returns only the memberdto : more optimized
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto?> GetMemberAsync(string username);
}
