﻿namespace DesignsAndBuild.Core.Services.Contract.AccountModuleContracts;

public interface IGoogleAuthService
{
    Task<AppUser> GoogleSignIn(GoogleSignInVM model);
}
