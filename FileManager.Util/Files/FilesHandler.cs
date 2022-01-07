using FileManager.Entities.Dtos;
using FileManager.Util.Extensions;

namespace FileManager.Util.Files;

public class FilesHandler
{
    public static async Task<dynamic> WriteFileOnServer(string path, FileUploadRequest file)
    {
        //Get data from the file
        var fileExtension = file.FileName.Split(".")[1];
        var fileNameWithouExtension = file.FileName.Split(".")[0];
        var fileNameWithextension = fileNameWithouExtension + "." + fileExtension;

        //Verify File type to save in correct folder
        var folder = fileExtension.ToLower() switch
        {
            "jpg" => "Images",
            "jpeg" => "Images",
            "png" => "Images",
            "mp4" => "Videos",
            "mkv" => "Videos",
            "3gp" => "Videos",
            "mp3" => "Music",            
            _ => "Docs"
        };
        string[] paths = { path, $"Uploads\\{folder}", fileNameWithextension };


        var fullPath = Path.Combine(paths);

        //Get file bytes
        var fileData = Convert.FromBase64String(file.Content.base64WithoutHeader());

        VerifyIfPathExists(fullPath);

        await File.WriteAllBytesAsync(fullPath, fileData);
        return new
        {
            FileName = fileNameWithouExtension,
            FileExtension = fileExtension,
            FileSize = fileData.Length
        };
    }

    private static void VerifyIfPathExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
