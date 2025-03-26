using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

//use the extension to make multiple builder.Services into just one
builder.Services.AddApplicationServices(builder.Configuration);


//used in [Authorize] endpoints
//also an extension
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();


//MIDDLEWARES
//this will catch all errors of middlewares after it
app.UseMiddleware<ExceptionMiddleware>();
//Cors should be first before the mapcontrollers
//withcors is the url of the frontend allowed to access
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
//authentication first before authorization
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//to use a service in programcs without dependency injection
// 'using' auto dispose it after using
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    //now we can use the datacontext
    var context = services.GetRequiredService<DataContext>();
    //create db if theres still no db
    //apply the migrations to db
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}


app.Run();
