using System;

namespace API.Entities;

public class AppUser
{
    // type 'prop' for boilerplate
    //int is primitive so it has a default of 0
    // 'Id' is convention so it will auto PK
    public int Id { get; set; }
    //this will warning since it can be null
    //so put required= it means we cant make user without username
    public required string UserName { get; set; }

    //required will cause error 404 in reesponse if theres no value sent
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}
