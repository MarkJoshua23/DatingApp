using API.Data;
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

var app = builder.Build();


//MIDDLEWARES
app.MapControllers();

app.Run();
