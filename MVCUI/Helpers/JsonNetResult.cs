using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Helpers
{
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings Setting { get; set; }

        public JsonNetResult()
        {
            Setting = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Error };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context.Equals(null))
                throw new ArgumentNullException("Context is Null. JsonSerializerSetting.JsonResult");

            if (this.JsonRequestBehavior == JsonRequestBehavior.AllowGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidOperationException("To Allow GET request, Set JsonResultBehavior to AllowGet");

            if (this.Data.Equals(null))
                return;

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = String.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            JsonSerializer serializer = JsonSerializer.Create(settings: this.Setting);
            using(JsonTextWriter writer =new JsonTextWriter(response.Output))
            {
                serializer.Serialize(writer, this.Data);
                writer.Flush();
            }
        }
    }
}
