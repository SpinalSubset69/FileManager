using AutoMapper;
using FileManager.Entities.Dtos;
using FileManager.Entities.Entities;

namespace FileManager.Api.UserEndpoints;

public static class UserApi
{
    public static void UserEndpoints(this WebApplication app)
    {        
        app.MapPost("/users", PostUser);
        app.MapGet("/users/{id}", FindUserById);
        app.MapPut("/users/{id}", UpdateUser);
        app.MapDelete("/users/{id}", DeleteUser);
        app.MapPost("/users/uploadfile/{id}", UploadFile);
        app.MapPut("/users/insertfileintofolder/{id}", InsertFileIntoFolder);
        app.MapGet("/users/userfolders/{id}", GetUserFolders);
        app.MapGet("/users/userfiles/{id}", GetUserFiles);
    }

    private static async Task<IResult> PostUser(RegisterUserDto user, UserService service, IMapper _mapper)
    {
        try
        {

            await service.SaveUserAsync(_mapper.Map<RegisterUserDto, User>(user));

            return Results.Ok("User Saved on DB");
        }catch(Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> FindUserById(int id, UserService service, IMapper _mapper)
    {
        try
        {
            var user = await service.FindUserById(id);
        
            return Results.Ok(new { message = "User", data = _mapper.Map<User, UserToReturnDto>(user)});
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateUser(int id, UpdateUserInfoDto request ,UserService service)
    {
        try
        {
            await service.UpdateUserInfo(id, request);

            return Results.Ok(new { message = "User Updated"});
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteUser(int id, UserService service)
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

    private static async Task<IResult> UploadFile(int id, FileUploadRequest file,UserService service, IWebHostEnvironment _host)
    {
        try
        {
            await service.UploadFile(id, file, _host.ContentRootPath);

            return Results.Ok(new { message = "File Uploaded" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertFileIntoFolder(int id, InsertFileInFolderRequest request, UserService service)
    {
        try
        {
            await service.InsertFileIntoFolder(request.FileId, request.FolderId, id);

            return Results.Ok(new { message = "File inserted on Folder" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetUserFolders(int id, UserService service)
    {
        try
        {
            var folders = await service.GetUserFilesOrFolders(id, "folders");

            return Results.Ok(new { message = "Folders", data = folders });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetUserFiles(int id, UserService service)
    {
        try
        {
            var files = await service.GetUserFilesOrFolders(id, "files");

            return Results.Ok(new { message = "Files", data = files });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

}
