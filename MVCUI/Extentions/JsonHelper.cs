using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MVCUI.Extentions
{
    public static class JsonHelper
    {
        public static String ToJson(this Object data) =>
            new JavaScriptSerializer().Serialize(data);
    }
}
