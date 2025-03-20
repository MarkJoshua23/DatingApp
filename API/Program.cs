using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
//dependecy injection will make an instance of the context, () is the options we can send to the dbcontext
builder.Services.AddDbContext<DataContext>(opt =>
{
    // send the connection string
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();


//scoped means the service is created once per request
//meaning after a successful login the token is sent then the service restart in next request
//<interface,service>
builder.Services.AddScoped<ITokenService,TokenService>();

var app = builder.Build();


//MIDDLEWARES
//Cors should be first before the mapcontrollers
//withcors is the url of the frontend allowed to access
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.MapControllers();

app.Run();
