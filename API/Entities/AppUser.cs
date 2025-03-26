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
    // public required byte[] PasswordHash { get; set; }

    //remove required only for seeding purposes
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public DateOnly DateOfBirth { get; set; }
    public required string KnownAs { get; set; }
    //date when the account is created
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public required string Gender { get; set; }
    public string? Introduction { get; set; }
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public List<Photo> Photos { get; set; } = [];
}
