using AutoMapper;

namespace FileManager.Api.FolderEndpoints;

public static class FolderApi
{
    public static void FolderEndpoints(this WebApplication app)
    {
        app.MapPost("/folders", PostFolder);
        app.MapGet("/folders/{userId}", GetUserFolders);
        app.MapPut("/folders/updatename/{id}", UpdateFolderName);
        app.MapPut("/folders/updatedesc/{id}", UpdateFolderDesc);
        app.MapDelete("/folders/{id}", DeleteFolder);
    }

    private static async Task<IResult> PostFolder(RegisterFolderDto request, FolderService service, IMapper _mapper)
    {

        try
        {

            await service.CreateUserFolder(_mapper.Map<RegisterFolderDto, Folder>(request));

            return Results.Ok("Folder Created on DB");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetUserFolders(int userId, FolderService service)
    {
        try
        {

            var folders = await service.FindFoldersBasedOnUserId(userId); 

            return Results.Ok(new { message = "Folders", data = folders });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateFolderName(int id, string name, FolderService service)
    {
        try
        {

            await service.UpdateFolderName(id, name);

            return Results.Ok(new { message = "Folder Name Updated"});
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateFolderDesc(int id, string desc, FolderService service)
    {
        try
        {

            await service.UpdateFolderDesc(id, desc);

            return Results.Ok(new { message = "Folder Description Updated" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteFolder(int id, FolderService service)
    {
        try
        {

            await service.DeleteFolder(id);

            return Results.Ok(new { message = "Folder Removed" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    
}
