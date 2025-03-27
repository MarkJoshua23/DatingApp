using System;
using API.DTOs;
using API.Entities;
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
        ));


        CreateMap<Photo, PhotoDto>();
    }
}
