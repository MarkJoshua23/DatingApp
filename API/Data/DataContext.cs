using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

//lightbulb then constructor with options
//lightbulb then use primary constructor
public class DataContext(DbContextOptions options) : DbContext(options)
{
    //if the name is Users then it will be the table name
    public DbSet<AppUser> Users { get; set; }
}
