using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVCUI.RSS
{
    public static class CryptoUtils
    {
        public static String SHA1(this String data)
        {
            using(SHA1Managed sha1=new SHA1Managed())
            {
                Byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
            }
        }
    }
}
