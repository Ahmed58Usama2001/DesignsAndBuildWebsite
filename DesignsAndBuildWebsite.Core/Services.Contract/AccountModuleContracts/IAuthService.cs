using DesignsAndBuild.Core.Entities.Identity.Gmail;

namespace DesignsAndBuild.Core.Services.Contract.AccountModuleContracts;

public interface IAuthService
{
    Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);

    Task<JwtResponseVM> SignInWithGoogle(GoogleSignInVM model);
}
