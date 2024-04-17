

namespace DesignsAndBuild.Core.Services.Contract.AccountModuleContracts;

public interface IFacebookAuthService
{
    Task<FacebookTokenValidationResponse> ValidateFacebookToken(string accessToken);
    Task<FacebookUserInfoResponse> GetFacebookUserInformation(string accessToken);
}
