using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{


    //inject dbcontext
    public static async Task SeedUsers(DataContext context)
    {
        //true if theres users in the table
        if (await context.Users.AnyAsync()) return;

        //get the json seed
        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        //so it will map to AppUser entity even if the case is different
        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        //map the json to AppUser entity
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);
        if(users ==null) return;

        foreach (var user in users)
        {
            //hmac should be inside the loop so theres new instance each loop
            //hmac is remade each loop because of 'new' so the key/salt is different
            using var hmac = new HMACSHA512();
            user.UserName = user.UserName.ToLower();
            //pass dummy password
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;

            //add the user in the transaction
            context.Users.Add(user);
        }

        //save the transaction/insert to db
        await context.SaveChangesAsync();
    }
}
