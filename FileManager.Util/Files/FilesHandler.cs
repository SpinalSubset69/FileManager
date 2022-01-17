using FileManager.Entities.Dtos;
using FileManager.Entities.Entities;
using FileManager.Util.Extensions;

namespace FileManager.Util.Files;

public class FilesHandler
{
    public static async Task<UserFile> WriteFileOnServer(string path, FileUploadRequest file)
    {
        //Get data from the file
        string fileExtension = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
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

    public static async Task<UserFile> SaveUserImageOnServer(User user, string path, FileUploadRequest file)
    {
        //Get data from the file
        string fileExtension = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
        string fileNameWithouExtension = Guid.NewGuid().ToString();
        var fileNameWithextension = fileNameWithouExtension + "." + fileExtension;

        //Verify File type to save in correct folder
        var folder = "ProfileImages";

        string[] basePaths = new[] { path, $"Uploads\\{folder}\\" };
        var fullPath = Path.Combine(basePaths);

        //Get file bytes
        byte[] fileData = Convert.FromBase64String(file.Content.base64WithoutHeader());

        VerifyIfPathExists(fullPath);


        await File.WriteAllBytesAsync(fullPath + fileNameWithextension, fileData);

        return new UserFile(fileNameWithouExtension, fileExtension, fileData.Length, DateTime.Now);

    }

    public static void DeleteFileOnServer(string path, string fileName, string fileExtension)
    {
        var folder = DefineFolderBasedOnFileExtension(fileExtension);

        var fullPath = Path.Combine(path, "Uploads" ,folder, fileName + "." + fileExtension);

        File.Delete(fullPath);
    }

    public static async Task<FileInfoResponse> GetFileBytes(string path, UserFile file)
    {
        var folder = DefineFolderBasedOnFileExtension(file.FileExtension);
        var fileName = $"{file.FileName}.{file.FileExtension}";
        var fullPath = Path.Combine(path, "Uploads" ,folder , fileName);
        var fileContent = await File.ReadAllBytesAsync(fullPath);
        var fileMimeType = DefineFileMimeType(file.FileExtension);

        return new FileInfoResponse(fullPath, fileMimeType, fileName, fileContent);   
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
        return extension.ToLower() switch
        {
            "jpg" => "Images",
            "jpeg" => "Images",
            "png" => "Images",
            "mp4" => "Videos",
            "mkv" => "Videos",
            "3gp" => "Videos",
            "mp3" => "Music",
            "exe" => "Executables",
            "msi" => "Executables",
            _ => "Docs"
        }; 
    }
    
    private static string DefineFileMimeType(string extension)
    {
        string mimeType = "";
        string[] imageTypes = new[]
        {
            "png",
            "jpg",
            "jpeg"
        };
        string[] docTypes = new[] { "pdf", "docx", "xlsx", "pptx" };
       
        
        if(imageTypes.Contains(extension))
        {
            mimeType = "image/" + extension;
        }
        
        //TODO: ADD DOCX MIME TYPE SUPport

        return mimeType;
    }
}
