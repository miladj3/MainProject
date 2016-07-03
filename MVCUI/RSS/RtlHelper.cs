using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVCUI.RSS
{
    public static class RtlHelper
    {
        private static readonly Regex MatchArabicHebrew =
           new Regex(@"[\u0600-\u06FF,\u0590-\u05FF]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static String CorrectRtl(this String title)
        {
            if (String.IsNullOrWhiteSpace(title))
                return String.Empty;

            const Char rleChar = (Char)0x202B;
            if (MatchArabicHebrew.IsMatch(title))
                return rleChar + title;
            return title;
        }

        public static String CorrectRtlBody(this String body)
        {
            if (String.IsNullOrWhiteSpace(body))
                return String.Empty;

            if (MatchArabicHebrew.IsMatch(body))
                return "<div style='text-align: right; font-family:tahoma; font-size:9pt;' dir='rtl'>" + body + "</div>";
            return "<div style='text-align: left; font-family:tahoma; font-size:9pt;' dir='ltr'>" + body + "</div>";
        }
    }
}
