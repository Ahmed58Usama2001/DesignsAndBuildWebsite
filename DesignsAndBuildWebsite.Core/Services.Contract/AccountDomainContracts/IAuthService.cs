namespace DesignsAndBuild.Core.Services.Contract.AccountModuleContracts;

public interface IAuthService
{
    Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);

    Task<bool> InvalidateSignedInTokenAsync(string token);

    Task<AppUser> SignInWithGoogle(GoogleSignInVM model);
    Task<JwtResponseVM> SignInWithFacebook(FacebookSignInVM model);

}
