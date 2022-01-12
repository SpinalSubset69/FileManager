using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Dtos.UserDtos;

public class RegisterUserDto
{
    [Required]
    [MinLength(4, ErrorMessage =("Min Length of 4 charactres"))]
    [MaxLength(20, ErrorMessage = ("Max Length of 20 charactres"))]
    public string UserName { get; set; }

    [Required]
    [Email]
    public string Email { get; set; }

    [Required]
       
    public string Password { get; set; }
}
