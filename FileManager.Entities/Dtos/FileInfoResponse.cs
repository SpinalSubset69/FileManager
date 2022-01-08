using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Dtos;

public class FileInfoResponse
{
    public string FileName { get; set; }
    public string MimeType { get; set; }
    public string FilePath { get; set; }
    public byte[] Content { get; set; }

    public FileInfoResponse(string filePath, string mimeType, string fileName, byte[] content)
    {
        FilePath = filePath;
        MimeType = mimeType;
        FileName = fileName;
        Content = content;
    }
}
