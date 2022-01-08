using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FolderController : ControllerBase
{
    private readonly FolderService _folderService;
    private readonly IMapper _mapper;

    public FolderController(FolderService folderService, IMapper mapper)
    {
        _folderService = folderService;
        _mapper = mapper;
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

    [HttpGet("{userId}")]
    public async Task<IResult> GetUserFolders(int userId)
    {
        try
        {

            var folders = await _folderService.FindFoldersBasedOnUserId(userId);

            return Results.Ok(new { message = "Folders", data = folders });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

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

            await _folderService.DeleteFolder(id);

            return Results.Ok(new { message = "Folder Removed" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("/files")]
    public async Task<IResult> GetFolderFiles(int id, int userId, FolderService service)
    {
        try
        {
            var files = await service.GetFolderFiles(id, userId);
            return Results.Ok(new { message = "Folder Files", data = files });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
