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
            await service.UploadFile(id, file, _host.WebRootPath);

            return Results.Ok(new { message = "Upload" });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

}
