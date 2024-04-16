using DesignsAndBuild.Core.Entities.Identity.Gmail;

namespace DesignsAndBuild.Core.Services.Contract.AccountModuleContracts;

public interface IGoogleAuthService
{
    Task<AppUser> GoogleSignIn(GoogleSignInDto model);
}
