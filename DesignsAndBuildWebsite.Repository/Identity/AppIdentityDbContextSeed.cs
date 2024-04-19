
namespace DesignsAndBuild.Repository.Identity;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {
        if(userManager.Users.Count()==0)
        {
            var user = new AppUser()
            {   
                Email = "ahmedusamasaad@gmail.com",
                UserName = "ahmedusamasaad",
                RegistrationDate= DateTime.Now
            };

            await userManager.CreateAsync(user,"Osama_58200165");
        }
    }
}
