using System;
using System.Web.Helpers;

namespace Utilities.Security
{
    public class Encryption
    {
        public static String EncryptingPassword(string password)
        {
            return Crypto.HashPassword(Crypto.SHA256(password));
        }

        public static Boolean VerifyPassword(string password, string hashedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, Crypto.SHA256(password));
        }
    }
}
