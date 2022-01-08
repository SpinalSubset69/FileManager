using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Util.Extensions;

public static class StringExtensions
{
    public static string base64WithoutHeader(this string content)
    {
        if(content.Contains("base64"))
        {
            return content.Split(",")[1];
        }          
        return content;
    }
}
