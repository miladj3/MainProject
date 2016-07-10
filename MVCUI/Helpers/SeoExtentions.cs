using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCUI.Helpers
{
    public static class SeoExtentions
    {
        #region Fields
        private const string SeparatorTitle = " - ";
        private const int MaxLenghtTitle = 60;
        private const int MaxLenghtSlug = 45;
        private const int MaxLenghtDescription = 170;
        #endregion

        #region MetaTang
        private const String faviconPath = "~/favicon.ico";

        public static String GenerateMetaTag(String title,
            String description,
            Boolean allowIndexPage,
            Boolean allowFollowLink,
            String author = "",
            String lastModified = "",
            String expire = "never",
            String lang = "fa",
            CacheControlType cacheControltype = CacheControlType.Private)
        {
            title = title.Substring(0, title.Length <= MaxLenghtTitle ? title.Length : MaxLenghtTitle).Trim();
            description = description.Substring(0, description.Length <= MaxLenghtDescription ? description.Length : MaxLenghtDescription).Trim();

            StringBuilder meta = new StringBuilder();
            meta.AppendLine($"<title>{ title }</title>");
            meta.AppendLine($@"<link rel=""shortcut icon"" href=""{ faviconPath }""/>");
            meta.AppendLine($@"<meta http-equiv=""content-language"" content=""{ lang }""/>");
            meta.AppendLine(@"<meta http-equiv=""content-type"" content=""text/html; charset=utf-8""/>");
            meta.AppendLine(@"<meta charset=""utf-8""/>");
            meta.AppendLine($@"<meta name=""description"" content=""{ description }""/>");
            meta.AppendLine($@"<meta http-equiv=""Cache-control"" content=""{ EnumHelper.GetEnumDescription(typeof(CacheControlType), cacheControltype.ToString()) }""/>");
            meta.AppendLine(String.Format(@"<meta name=""robots"" content=""{0}, {1}"" />", allowIndexPage ? "index" : "noindex", allowFollowLink ? "follow" : "nofollow"));
            meta.AppendLine($@"<meta name=""expires"" content=""{ expire }""/>");
            if (!String.IsNullOrEmpty(lastModified))
                meta.AppendLine($@"<meta name=""last-modified"" content=""{ lastModified }"">");
            if (!String.IsNullOrEmpty(author))
                meta.AppendLine($@"<meta name=""author"" content=""{ author }\"">");

            return meta.ToString();
        }
        #endregion

        #region Slug For Url In Address Bar
        public static String GenerateSlug(this String url)
        {
            String slug = RemoveDiacriticsFromString(url).ToLowerInvariant();
            slug = Regex.Replace(slug, @"[^a-z0-9-\u0600-\u06FF]", "-");
            slug = Regex.Replace(slug, @"\s+", "-").Trim();
            slug = Regex.Replace(slug, @"-+", "-");
            slug = slug.Substring(0, slug.Length <= MaxLenghtSlug ? slug.Length : MaxLenghtSlug).Trim();

            return slug;
        }
        #endregion

        #region Title
        /// <summary>
        /// حذف اَعراب از حروف و کلمات
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String RemoveDiacriticsFromString(this String data)
        {
            if (String.IsNullOrEmpty(data))
                return data;

            String _data = data.Normalize(NormalizationForm.FormKC);
            StringBuilder strBuilder = new StringBuilder();
            foreach (Char i in _data)
            {
                var _unicode = CharUnicodeInfo.GetUnicodeCategory(i);
                if (_unicode != UnicodeCategory.NonSpacingMark)
                    strBuilder.Append(i);
                else
                {
                    //اسامی مانند آفتاب نباید خراب شوند
                    if (i == 1619) //آ
                    {
                        strBuilder.Append(i);
                    }
                }
            }
            return strBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// یکسان سازی "ی" و "ک" دریافتی
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String SafeFarsiStrWhenSaveInDB(this String data)
        {
            if (string.IsNullOrEmpty(data))
                return data;
            return data.Replace("ی", "ی").Replace("ک", "ک");
        }

        public static String ResolveTitleForUrl(this HtmlHelper htmlHelper, String title)
        {
            return string.IsNullOrEmpty(title)
                ? string.Empty
                : Regex.Replace(Regex.Replace(title, "[^\\w]", "-"), "[-]{2,}", "-");
        }

        public static String ResolveTitleForUrl(String title)
        {
            return string.IsNullOrEmpty(title)
                ? string.Empty
                : Regex.Replace(Regex.Replace(title, "[^\\w]", "-"), "[-]{2,}", "-");
        }

        public static String RemoveAccent(this String text)
        {
            Byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }

        public static String RemoveHtmlTags(this String text)
        {
            return String.IsNullOrEmpty(text) ? String.Empty : Regex.Replace(text, @"<(.|\n)*?>", String.Empty);
        }

        public static String GeneratePageTitle(params String[] crumbs)
        {
            StringBuilder title = new StringBuilder();

            for (Int32 i = 0; i < crumbs.Length; i++)
                title.Append(String.Format("{0}{1}", crumbs[i], (i < crumbs.Length - 1) ? SeparatorTitle : String.Empty));

            String result=title.ToString().Substring(0, title.Length <= MaxLenghtTitle ? title.Length : MaxLenghtTitle).Trim();

            return result;
        }

        public static String GeneratePageDescription(String description)
        {
            return
                description.Substring(0,
                    description.Length <= MaxLenghtDescription ? description.Length : MaxLenghtDescription).Trim();
        }

        #endregion
    }
}
