using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Dtos;

public class PaginationParams
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}
