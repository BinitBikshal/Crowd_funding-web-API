

using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Crowd_fund.EndPoints
{
    public class UserEndPoint : BaseEndPoint
    {
        public UserEndPoint()
        {
            url = baseUrl + "User";
        }
        public override void MapEndPoints(WebApplication app)
        {
            app.MapGet(url, Getall).Produces(StatusCodes.Status200OK);
            app.MapPost($"{url}/setup", Setup).Produces(StatusCodes.Status200OK);
        }

        private async Task<IResult> Setup(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var adminUser =new ApplicationUser { UserName = "Admin", Email = "Admin@selfpractice.com" };
            var password = "Test85*";
            var roleName = "admin";
            var result = await userManager.CreateAsync(adminUser, password);
            if (result.Succeeded)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role is null)
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                    result = await userManager.AddToRoleAsync(adminUser, roleName);
                }
            }
            return Results.Ok(result);
        }

        private async Task<IResult> Getall(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            return Results.Ok(await applicationDbContext.Users.ToListAsync());
        }
    }
}
