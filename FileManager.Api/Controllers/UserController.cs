using AutoMapper;
using FileManager.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly UserService _userService;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _host;

    public UserController(UserService userService, IMapper mapper, IWebHostEnvironment host)
    {
        _userService = userService;
        _mapper = mapper;
        _host = host;
    }

    [HttpPost]
    public async Task<IResult> PostUser([FromBody]RegisterUserDto user)
    {
        try
        {

            await _userService.SaveUserAsync(_mapper.Map<RegisterUserDto, User>(user));

            return Results.Ok("User Saved on DB");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> FindUserById(int id)
    {
        try
        {
            var user = await _userService.FindUserById(id);

            return Results.Ok(new { message = "User", data = _mapper.Map<User, UserToReturnDto>(user) });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    
    [HttpPut]
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

    [HttpDelete]
    public async Task<IResult> DeleteUser(int id, UserService service)
    {
        try
        {
            await service.DeleteUser(id);

            return Results.Ok(new { message = "User Deleted" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("/uploadfile/{id}")]
    public async Task<IResult> UploadFile(int id, [FromBody]FileUploadRequest file)
    {
        try
        {
            await _userService.UploadFile(id, file, _host.ContentRootPath);

            return Results.Ok(new { message = "File Uploaded" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPut("/inertfileintofolder/{id}")]
    public async Task<IResult> InsertFileIntoFolder(int id, [FromBody]InsertFileInFolderRequest request)
    {
        try
        {
            await _userService.InsertFileIntoFolder(request.FileId, request.FolderId, id);

            return Results.Ok(new { message = "File inserted on Folder" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("/userfolders/{id}")]
    public async Task<IResult> GetUserFolders(int id)
    {
        try
        {
            var folders = await _userService.GetUserFilesOrFolders(id, "folders");

            return Results.Ok(new { message = "Folders", data = folders });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("/userfiles/{id}")]
    public async Task<IResult> GetUserFiles(int id)
    {
        try
        {
            var files = await _userService.GetUserFilesOrFolders(id, "files");
            return Results.Ok(new { message = "Files", data = files });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


}
