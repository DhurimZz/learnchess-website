using Microsoft.AspNetCore.Identity;

namespace learnchess.Areas.Identity.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {

            if (!await roleManager.RoleExistsAsync(Enums.Roles.Admin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            }
            if (!await roleManager.RoleExistsAsync(Enums.Roles.User.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));
            }
        }
    }
}
