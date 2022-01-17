using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Dtos;

public class PaginationDto<T>
{
    public int  PageSize { get; set; }
    public int PageIndex { get; set; }
    public IEnumerable<T> Items { get; set; }
    public int Count { get; set; }

    public PaginationDto(IEnumerable<T> items, int pageIndex, int pageSize, int count)
    {
        Items = items;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
    }
}
