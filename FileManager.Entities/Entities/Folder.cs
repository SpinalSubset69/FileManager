using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Entities;

public class Folder : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Created_At { get; set; }
    public int UserId{ get; set; }
}
