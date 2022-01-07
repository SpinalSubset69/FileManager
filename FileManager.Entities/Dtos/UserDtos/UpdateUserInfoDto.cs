using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Dtos.UserDtos;

public class UpdateUserInfoDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
}
