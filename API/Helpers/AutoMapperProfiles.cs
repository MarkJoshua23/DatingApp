using System;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles: Profile
{
    //make a constructor
    public AutoMapperProfiles()
    {
        //<From, To>  
        CreateMap<AppUser, MemberDto>()  
        // Configure mapping for MemberDto  
        // We need to populate PhotoUrl with the URL of the photo where isMain is true  
        //MapFrom to determine where to get the value that will map to PhotoUrl
        .ForMember(d => d.PhotoUrl, o => o.MapFrom(  
            // 's' represents the source object (AppUser) where we would map from  
            // Populate PhotoUrl with the Url of the first photo where IsMain is true  
            s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url  
        ))
        //populate age with values
        .ForMember(d => d.Age, o => o.MapFrom(  
            // 's' represents the source object (AppUser) where we would map from  
            //use the calculateage emthod from extension
            //use the value to populate age
            s => s.DateOfBirth.CalculateAge() 
        ));
        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDto,AppUser>();
        CreateMap<RegisterDto,AppUser>();
        //for automapper to know what to do for string to dateonly conversions
        //take the string and parse it to dateonly
        CreateMap<string,DateOnly>().ConvertUsing(s=> DateOnly.Parse(s));
    }
}
