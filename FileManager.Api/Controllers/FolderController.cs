using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Api.Controllers;

[ApiController, Authorize]
[Route("[controller]")]
public class FolderController : ControllerBase
{
    private readonly FolderService _folderService;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _host;

    public FolderController(FolderService folderService, IMapper mapper, IWebHostEnvironment host)
    {
        _folderService = folderService;
        _mapper = mapper;
        _host = host;
    }


    [HttpPost]
    public async Task<IResult> PostFolder(RegisterFolderDto request)
    {

        try
        {

            await _folderService.CreateUserFolder(_mapper.Map<RegisterFolderDto, Folder>(request));

            return Results.Ok("Folder Created on DB");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    //[HttpGet("{userId}")]
    //public async Task<IResult> GetUserFolders(int userId)
    //{
    //    try
    //    {

    //        var folders = await _folderService.FindFoldersBasedOnUserId(userId);

    //        return Results.Ok(new { message = "Folders", data = folders });
    //    }
    //    catch (Exception ex)
    //    {
    //        return Results.Problem(ex.Message);
    //    }
    //}

    [HttpPut("/updatename/{id}")]
    public async Task<IResult> UpdateFolderName(int id, [FromBody]string name)
    {
        try
        {

            await _folderService.UpdateFolderName(id, name);

            return Results.Ok(new { message = "Folder Name Updated" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPut("/updatedesc/{id}")]
    public async Task<IResult> UpdateFolderDesc(int id, string desc)
    {
        try
        {

            await _folderService.UpdateFolderDesc(id, desc);

            return Results.Ok(new { message = "Folder Description Updated" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteFolder(int id)
    {
        try
        {

            await _folderService.DeleteFolder(id, _host.ContentRootPath);

            return Results.Ok(new { message = "Folder Removed" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("/files/{id}")]
    public async Task<IResult> GetFolderFiles(int id)
    {
        try
        {
            var files = await _folderService.GetFolderFiles(id);
            return Results.Ok(new { message = "Folder Files", data = files });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
