using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Dtos.UserDtos;

public class UserToReturnDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime Created_At { get; set; }
    public string? ProfileImage { get; set; }

    public UserToReturnDto(int id, string userName, string email, DateTime created_At, string? profileImage)
    {
        Id = id;
        UserName = userName;
        Email = email;
        Created_At = created_At;
        ProfileImage = profileImage;
    }
}
