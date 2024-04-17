namespace DesignsAndBuild.APIs.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAuthService _authService;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IAuthService authService,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            }); ;
        }

        return Unauthorized(new ApiResponse(401));
    }

    [HttpPost("forgetPassword")]
    public async Task<ActionResult<UserDto>> ForgetPassword(ForgetPasswordDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);


            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                var email = new Email()
                {
                    Title = "Reset Password",
                    To = model.Email,
                    Body = resetPasswordLink
                };
                EmailSettings.SendEmail(email);
                return Ok(model);
            }
            return Unauthorized(new ApiResponse(401));
        }

        return Ok(model);
    }

    [HttpPost("ResetPassword")]
    public async Task<ActionResult<UserDto>> ResetPassword(ResetPasswordDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);


            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (result.Succeeded)
                    return Ok(model);
                string errors = string.Join(", ", result.Errors.Select(error => error.Description));
                return BadRequest(new ApiResponse(400, errors));

            }
        }

        return Ok(model);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto model)
    {
        if (CheckEmailExists(model.Email).Result.Value)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This email already exists!" } });

        var user = new AppUser
        {
            Email = model.Email,
            UserName=model.Email.Split('@').First(),
            RegistrationDate = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            string errors = string.Join(", ", result.Errors.Select(error => error.Description));
            return BadRequest(new ApiResponse(400, errors));
        }

        return Ok(new UserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Token = await _authService.CreateTokenAsync(user, _userManager)
        });

    }


    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var user = await _userManager.FindByEmailAsync(email);

        return Ok(new UserDto()
        {
            UserName = user.UserName,
            Email = user.Email,
            Token = await _authService.CreateTokenAsync(user, _userManager)
        });
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExists(string email)
        => await _userManager.FindByEmailAsync(email) is not null;

    [HttpPost("CreateRole")]
    public async Task<ActionResult> CreateToken(string Name)
    {
        try
        {
            if (string.IsNullOrEmpty(Name)) return BadRequest(new ApiResponse(400, "Role cannot be Empty !!"));

            bool isRoleAlreadyExists = await _roleManager.RoleExistsAsync(Name);
            if (isRoleAlreadyExists) return BadRequest(new ApiResponse(400, $"Role: {Name} Already Exists !!"));

            await _roleManager.CreateAsync(new IdentityRole(Name));
            return Ok(Name);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return BadRequest(new ApiResponse(400));
        }
    }

    [HttpPost("GoogleSignIn")]
    public async Task<IActionResult> GoogleSignIn(GoogleSignInVM model)
    {
        try
        {
            return (IActionResult)await _authService.SignInWithGoogle(model);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    [HttpPost("FacebookSignIn")]
    public async Task<IActionResult> FacebookSignIn(FacebookSignInVM model)
    {
        try
        {
            return (IActionResult)await _authService.SignInWithFacebook(model);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}