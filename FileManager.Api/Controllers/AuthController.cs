using FileManager.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController: ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthService _authService;
    private readonly IConfiguration _config;

    public AuthController(UserService userService, AuthService authService, IConfiguration config)
    {
        _userService = userService;
        _authService = authService;
        _config = config;
    }

    [HttpPost("/login")]
    public async Task<IResult> Login([FromBody]LoginDto request)
    {
        try
        {
            var user = await _userService.VerifyLoginInfo(request);
            var session = _authService.CreateSession(user, _config.GetSection("Cryptography:securityKey").Value);
            Response.ContentType = "application/json";
            return Results.Ok(new { message = "Login", data = session });

        }catch(Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("/signup")]    
    public async Task<IResult> PostUser([FromBody] RegisterUserDto user)
    {
        try
        {

            await _userService.SaveUserAsync(user);

            return Results.Ok("User Saved on DB");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
