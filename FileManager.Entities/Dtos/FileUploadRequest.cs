﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Dtos;

public class FileUploadRequest
{
    public string FileName { get; set; }    
    public string Content { get; set; }

}
