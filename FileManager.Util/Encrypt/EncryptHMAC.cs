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
        public static void GetHMAC512(string plainText, out byte[] hashedText, out byte[] hashedSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                hashedSalt = hmac.Key;
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                hashedText = computeHash;
            }
        }

        public static bool CompareHMAC512(string textToCompare, byte[] hashedText, byte[] hashedSalt)
        {
            using(var hmac = new HMACSHA512(hashedSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(textToCompare));
                return computeHash.SequenceEqual(hashedText);
            }
        }
    }
}
