using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Util.Extensions;

public static class BytesExtensions
{
    public static string ByteArrayToString(this byte[] stream)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < stream.Length; i++)
        {
            sb.AppendFormat("{0:x2}", stream[i]);
        }

        return sb.ToString();
    }
}
