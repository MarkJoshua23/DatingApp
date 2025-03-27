using System;

namespace API.DTOs;
//return type of the UserCOntroller
public class MemberDto
{
    //remove required to all


    public int Id { get; set; }
    //automapper can map with different case
    public string? Username { get; set; }
    //Automapper will run the GetAge since it has Age
    public int Age { get; set; }
    public string? PhotoUrl { get; set; }
    public string? KnownAs { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string? Gender { get; set; }
    public string? Introduction { get; set; }
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public List<PhotoDto>? Photos { get; set; }

}
