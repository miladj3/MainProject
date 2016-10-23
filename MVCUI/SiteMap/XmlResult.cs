using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace MVCUI.SiteMap
{
    public class XmlResult : ActionResult
    {

        #region Fields
        private readonly Object _objectToSerial;
        #endregion

        #region Constracture
        public XmlResult(Object objectToSerial)
        {
            _objectToSerial = objectToSerial;
        }
        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            if (_objectToSerial == null)
                return;

            context.HttpContext.Response.Clear();
            XmlSerializer _xmlSerializer = new XmlSerializer(_objectToSerial.GetType());
            context.HttpContext.Response.ContentType = "text/xml";
            _xmlSerializer.Serialize(context.HttpContext.Response.Output, _objectToSerial);
        }
    }
}