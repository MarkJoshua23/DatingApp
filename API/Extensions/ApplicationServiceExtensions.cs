using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;
//static allow use of method without making instance
public static class ApplicationServiceExtensions
{
    //Iservice if we want to make an extension of builder.Services.
    //'this' means its an extension of .Services in program.cs so it will be .Services.AddApplicationServices
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        //dependecy injection will make an instance of the context, () is the options we can send to the dbcontext
        services.AddDbContext<DataContext>(opt =>
        {
            // send the connection string
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();


        //scoped means the service is created once per request
        //meaning after a successful login the token is sent then the service restart in next request
        //<interface,service>
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
