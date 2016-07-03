using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowUploadSpecialFilesAttribute : ActionFilterAttribute
    {
        private readonly String _whiteListEX;
        private readonly IList<String> _blackListToFilter = new List<String>();

        public AllowUploadSpecialFilesAttribute(String whiteListExtension)
        {
            if (String.IsNullOrEmpty(whiteListExtension))
                throw new ArgumentNullException("لیست پسوند فایل های مجاز خالی مبیاشد");

            _whiteListEX = whiteListExtension;
            String[] _extList = _whiteListEX.Split(',');
            foreach (String item in _extList.Where(x => !String.IsNullOrWhiteSpace(x)))
                _blackListToFilter.Add(item.ToLowerInvariant().Trim());
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpFileCollectionBase file = filterContext.HttpContext.Request.Files;
            foreach (String item in file)
            {
                HttpPostedFileBase _file = file[item];
                if (_file == null || _file.ContentLength == 0)
                    continue;
                if (!IsImageFile(_file))
                    return;
                if (!CanUpload(_file.FileName))
                    throw new InvalidOperationException($"شما اجازه بارگزاری { Path.GetExtension(_file.FileName) } را ندارید.\nشما فقط مجاز با اپلود فایل های { _whiteListEX } میباشید.");
            }
            base.OnActionExecuting(filterContext);
        }

        private Boolean IsImageFile(HttpPostedFileBase _file)
        {
            using (Image img = Image.FromStream(_file.InputStream))
                return img.Width > 0;
        }

        private Boolean CanUpload(String fileName)
        {
            if (!String.IsNullOrWhiteSpace(fileName))
                return false;
            String _extFile = Path.GetExtension(fileName.ToLowerInvariant());
            return _blackListToFilter.Contains(_extFile);
        }
    }
}
