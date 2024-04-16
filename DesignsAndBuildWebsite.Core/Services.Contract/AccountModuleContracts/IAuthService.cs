namespace DesignsAndBuild.Core.Services.Contract.AccountModuleContracts;

public interface IAuthService
{
    Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
}
