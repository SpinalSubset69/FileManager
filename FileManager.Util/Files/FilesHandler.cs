using FileManager.Entities.Dtos;
using FileManager.Entities.Entities;
using FileManager.Util.Extensions;

namespace FileManager.Util.Files;

public class FilesHandler
{
    public static async Task<UserFile> WriteFileOnServer(string path, FileUploadRequest file)
    {
        //Get data from the file
        string fileExtension = file.FileName.Split(".")[1];
        string fileNameWithouExtension = file.FileName.Split(".")[0];
        var fileNameWithextension = fileNameWithouExtension + "." + fileExtension;

        //Verify File type to save in correct folder
        var folder = DefineFolderBasedOnFileExtension(fileExtension);

        string[] basePaths = new []{ path, $"Uploads\\{folder}\\" };
        var fullPath = Path.Combine(basePaths);

        //Get file bytes
        byte[] fileData = Convert.FromBase64String(file.Content.base64WithoutHeader());

        VerifyIfPathExists(fullPath);
       

        await File.WriteAllBytesAsync(fullPath + fileNameWithextension, fileData);
        
        return new UserFile(fileNameWithouExtension, fileExtension, fileData.Length, DateTime.Now);
       
    }

    private static void VerifyIfPathExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private static string DefineFolderBasedOnFileExtension(string extension)
    {
        string folder = extension.ToLower() switch
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

        return folder;
    }
}
