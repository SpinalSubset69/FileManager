using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Entities;

public class UserFile : BaseEntity
{
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public DateTime Created_At { get; set; }
    public double FileSize { get; set; }
    public int UserId { get; set;}
    public int? FolderId { get; set; }

    public UserFile(string fileName, string fileExtension, double fileSize, DateTime created_At)
    {
        FileName = fileName;
        FileExtension = fileExtension;
        FileSize = fileSize;
        Created_At = created_At;        
    }
    public UserFile()
    {

    }

}
