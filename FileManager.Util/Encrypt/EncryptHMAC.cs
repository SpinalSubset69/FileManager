using FileManager.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Util.Encrypt
{
    public class EncryptHMAC
    {
        public static void GetHMAC512(string plainText, out string hashedText, out string hashedSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                hashedSalt = hmac.Key.ByteArrayToString();
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                hashedText = computeHash.ByteArrayToString();
            }
        }

        public static bool CompareHMAC512(string textToCompare, string hashedText, string hashedSalt)
        {
            using(var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(hashedSalt)))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(textToCompare));
                return computeHash.SequenceEqual(Encoding.UTF8.GetBytes(hashedText));
            }
        }
    }
}
