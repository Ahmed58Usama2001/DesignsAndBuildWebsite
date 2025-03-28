﻿namespace DesignsAndBuild.APIs.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAuthService _authService;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IMaillingService _mailService;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IAuthService authService,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IMaillingService mailService
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
        _roleManager = roleManager;
        _configuration = configuration;
        _mailService = mailService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (CheckEmailExists(model.Email).Result.Value)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This email already exists!" } });

        var user = new AppUser
        {
            Email = model.Email,
            UserName = model.Email.Split('@').First(),
            RegistrationDate = DateTime.Now,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            string errors = string.Join(", ", result.Errors.Select(error => error.Description));
            return BadRequest(new ApiResponse(400, errors));
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = UrlEncoder.Default.Encode(code) }, "http", _configuration["AngularBaseUrl"]);

        var bodyUrl = $"{Directory.GetCurrentDirectory()}\\wwwroot\\TempleteHtml\\2-StepVerificationTemplete.html";
        var body = new StreamReader(bodyUrl);
        var mailText = body.ReadToEnd();
        body.Close();

        mailText = mailText.Replace("[username]", user.UserName).Replace("[LinkHere]",
            HtmlEncoder.Default.Encode(callbackUrl));

        var emailResult = await _mailService.SendEmailAsync(model.Email, "Confirm Email", mailText);
        if (emailResult == false)
            return BadRequest(new ApiResponse(400));

        return Ok(true);

    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId,string code)
    {
        if (userId == null || code == null)
            return BadRequest(new ApiResponse(400));

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            Unauthorized(new ApiResponse(401));

        var decodedCode = System.Web.HttpUtility.UrlDecode(code);
        var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

        var status = result.Succeeded ? true
            : false;

        return Ok(status);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized(new ApiResponse(401));

            if(!user.EmailConfirmed)
                return Unauthorized(new ApiResponse(401,"Email Needs to be confirmed"));


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
    public async Task<ActionResult> CreateRole(string Name)
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
    public async Task<ActionResult<UserDto>> GoogleSignIn(GoogleSignInVM model)
    {
        try
        {
            var result = await _authService.SignInWithGoogle(model);
            if (result != null)
            {
                UserDto userDto = new UserDto()
                {
                    UserName=result.UserName??string.Empty,
                    ProfilePictureUrl=result.ProfilePictureUrl,
                    Email=result.Email ?? string.Empty,
                    Token = await _authService.CreateTokenAsync(result, _userManager)
                };

                return Ok(userDto);
            }
            else
                return BadRequest(new ApiResponse(400));
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest(new ApiResponse(400));
        }
    }

    [HttpPost("FacebookSignIn")]
    public async Task<ActionResult<UserDto>> FacebookSignIn(FacebookSignInVM model)
    {
        try
        {
            var result= await _authService.SignInWithFacebook(model);
            if (result != null)
            {
                UserDto userDto = new UserDto()
                {
                    UserName = result.UserName ?? string.Empty,
                    ProfilePictureUrl = result.ProfilePictureUrl,
                    Email = result.Email ?? string.Empty,
                    Token = await _authService.CreateTokenAsync(result, _userManager)
                };

                return Ok(userDto);
            }
            else
                return BadRequest(new ApiResponse(400));
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest(new ApiResponse(400));
        }
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
                var resetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, "http", _configuration["AngularBaseUrl"]);

                //var resetPasswordLink = "www.facebook.com"; 

                var bodyUrl = $"{Directory.GetCurrentDirectory()}\\wwwroot\\TempleteHtml\\ForgetPasswordTemplete.html";
                var body = new StreamReader(bodyUrl);
                var mailText = body.ReadToEnd();
                body.Close();

                mailText = mailText.Replace("[username]", user.UserName).Replace("[LinkHere]", resetPasswordLink);

                var result = await _mailService.SendEmailAsync(model.Email, "Reset Password", mailText);
                if (result == false)
                    return BadRequest(new ApiResponse(400, "No Internet Connection"));


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

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            try
            {
                await _authService.InvalidateSignedInTokenAsync(token); // Call your AuthService method
                return Ok(new { message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message); // Log the error
                return BadRequest(new { message = "Error during logout" });
            }
        }

        return BadRequest(new { message = "Unable to logout" });
    }


}