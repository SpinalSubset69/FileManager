using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Dtos;

public class Session
{
    public string Token { get; set; }
    public string ExpiresIn { get; set; }

    public Session(string expiresIn, string token)
    {
        ExpiresIn = expiresIn;
        Token = token;
    }
}
