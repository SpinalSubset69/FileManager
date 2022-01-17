using AutoMapper;
using FileManager.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly UserService _userService;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _host;
    private readonly AuthService _authService;
    private readonly IConfiguration _configuration;

    public UserController(UserService userService, IMapper mapper, IWebHostEnvironment host, AuthService authService, IConfiguration configuration)
    {
        _userService = userService;
        _mapper = mapper;
        _host = host;
        _authService = authService;
        _configuration = configuration;
    }
    
    [HttpGet, Authorize]
    public async Task<IResult> FindUserById()
    {
        try
        {
            //At this point token has been validated so we can decode it without validation
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenInfo = _authService.DecodeToken(token);
            var user = await _userService.FindUserById(Convert.ToInt32(tokenInfo.Issuer));
            return Results.Ok(new { message = "User", data = _mapper.Map<User, UserToReturnDto>(user) });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("/folders/"), Authorize]
    public async Task<IResult> GetUserFolders([FromQuery] PaginationParams pagParams)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenInfo = _authService.DecodeToken(token);
            var folders = await _userService.GetUserFilesOrFolders(Convert.ToInt32(tokenInfo.Issuer), "folders", pagParams);            

            return Results.Ok(new { message = "Folders", data = folders });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("/userfiles/"), Authorize, Authorize]
    public async Task<IResult> GetUserFiles([FromQuery]PaginationParams pagParams)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenInfo = _authService.DecodeToken(token);
            var files = await _userService.GetUserFilesOrFolders(Convert.ToInt32(tokenInfo.Issuer), "files", pagParams);            
            return Results.Ok(new { message = "Files", data = files });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("/queryonfiles"), Authorize, Authorize]
    public async Task<IResult> QueryOnUserFiles([FromQuery] string query)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenInfo = _authService.DecodeToken(token);
            var files = await _userService.QueryOnFilesAsync(Convert.ToInt32(tokenInfo.Issuer), query);            
            return Results.Ok(new { message = "Files", data = files });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("/download/file/{id}")]
    public async Task<dynamic> GetFile(int id)
    {
        try
        {
            var file = await _userService.GetFileStreamBasedOnId(id, _host.WebRootPath);
            return File(file.Content, "application/octet-stream", file.FileName);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPut, Authorize]
    public async Task<IResult> UpdateUser(int id, [FromBody]UpdateUserInfoDto request)
    {
        try
        {
            await _userService.UpdateUserInfo(id, request);

            return Results.Ok(new { message = "User Updated" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpDelete("{id}"), Authorize]
    public async Task<IResult> DeleteUser(int id, UserService service)
    {
        try
        {
            await service.DeleteUser(id, _host.WebRootPath);

            return Results.Ok(new { message = "User Deleted" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpDelete("/file/{id}")]
    public async Task<IResult> DeleteUserFile(int id)
    {
        try
        {
            await _userService.DeleteUserFileAsync(id, _host.WebRootPath);

            return Results.Ok(new { message = "File Removed" });
        }catch(Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("/uploadfile"), Authorize]
    public async Task<IResult> UploadFile([FromBody]FileUploadRequest file)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenInfo = _authService.DecodeToken(token);
            var fileInfo = await _userService.UploadFile(Convert.ToInt32(tokenInfo.Issuer), file, _host.WebRootPath);

            return Results.Ok(new { message = "File Uploaded", data = fileInfo });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("/userimage"), Authorize]
    public async Task<IResult> UploadUserImage([FromBody] FileUploadRequest file)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenInfo = _authService.DecodeToken(token);
            await _userService.SaveUserImageAsync(Convert.ToInt32(tokenInfo.Issuer), file, _host.WebRootPath);

            return Results.Ok(new { message = "File Uploaded" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPut("/insertfileintofolder"), Authorize]
    public async Task<IResult> InsertFileIntoFolder( [FromBody]InsertFileInFolderRequest request)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenInfo = _authService.DecodeToken(token);
            await _userService.InsertFileIntoFolder(request.FileId, request.FolderId, Convert.ToInt32(tokenInfo.Issuer));

            return Results.Ok(new { message = "File inserted on Folder" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

   


}
