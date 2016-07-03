using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUI.Searching
{
    public static class CorrectPersianForLucene
    {
        /// <summary>
        /// درست کردن " ی " و " ک " در لوسین
        /// </summary>
        public const char ArabicYeChar = (char)1610;
        public const char PersianYeChar = (char)1740;

        public const char ArabicKeChar = (char)1603;
        public const char PersianKeChar = (char)1705;

        public static string ApplyCorrectPersianLucene(this String data)
        {
            if (String.IsNullOrWhiteSpace(data))
                return String.Empty;

            return data.Replace(ArabicYeChar, PersianYeChar).Replace(ArabicKeChar, PersianKeChar).Trim();
        }
    }
}
