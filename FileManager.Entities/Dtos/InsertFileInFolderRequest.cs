using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Dtos;

public class InsertFileInFolderRequest
{
    public int FileId { get; set; }     
    public int FolderId { get; set; }
}
