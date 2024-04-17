using StackExchange.Redis;

namespace DesignsAndBuild.Service.AuthModuleService;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly DesignsAndBuildContext _context;
    private readonly IGoogleAuthService _googleAuthService;
    private readonly IFacebookAuthService _facebookAuthService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConnectionMultiplexer _redis;


    public AuthService(IConfiguration configuration,
        DesignsAndBuildContext context,
         UserManager<AppUser> userManager,
        IGoogleAuthService googleAuthService,
        IFacebookAuthService facebookAuthService,
        IConnectionMultiplexer redis)
    {
        _context = context;
        _googleAuthService = googleAuthService;
        _userManager = userManager;
        _configuration = configuration;
        _facebookAuthService = facebookAuthService;
        _redis = redis;
    }


    public async Task<JwtResponseVM> SignInWithGoogle(GoogleSignInVM model)
    {
        var response = await _googleAuthService.GoogleSignIn(model);

        if (response is null)
            return null;

        var jwtResponse = await CreateTokenAsync(response,_userManager);

        var data = new JwtResponseVM
        {
            Token = jwtResponse,
        };

        return data;
    }

    public async Task<JwtResponseVM> SignInWithFacebook(FacebookSignInVM model)
    {
        var validatedFbToken = await _facebookAuthService.ValidateFacebookToken(model.AccessToken);

        if (validatedFbToken is null)
            return null;

        var userInfo = await _facebookAuthService.GetFacebookUserInformation(model.AccessToken);

        if (userInfo is null)
            return null;

        var userToBeCreated = new CreateUserFromSocialLogin
        {
            UserName = userInfo.Name,
            Email = userInfo.Email,
            ProfilePicture = userInfo.Picture.Data.Url.AbsoluteUri,
            LoginProviderSubject = userInfo.Id,
        };

        var user = await _userManager.CreateUserFromSocialLogin(_context, userToBeCreated, LoginProvider.Facebook);

        if (user is not null)
        {
            var jwtResponse = await CreateTokenAsync(user,_userManager);

            var data = new JwtResponseVM
            {
                Token = jwtResponse,
            };

            return data;
        }
        else
        return null;

    }

    public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
    {
        // Private claims (user-defined)
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.GivenName, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var userRoles = await userManager.GetRolesAsync(user);

        foreach (var role in userRoles)
            authClaims.Add(new Claim(ClaimTypes.Role, role));

        var secretKey = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);
        var requiredKeyLength = 256 / 8; // 256 bits
        if (secretKey.Length < requiredKeyLength)
        {
            // Pad the key to meet the required length
            Array.Resize(ref secretKey, requiredKeyLength);
        }

        var token = new JwtSecurityToken(
            audience: _configuration["JWT:ValidAudience"],
            issuer: _configuration["JWT:ValidIssuer"],
            expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
            claims: authClaims,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> InvalidateSignedInTokenAsync(string token)
    {
        try
        {
            var redis = _redis.GetDatabase(); // Access the Redis database using dependency injection

            var key = $"blacklisted_token:{token}"; // Use a descriptive key format
            var expiration = TimeSpan.FromDays(1); // Set expiration for 1 day

            var added = await redis.StringSetAsync(key, string.Empty, expiration, (When)CommandFlags.None);

            return added;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }
}
