using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCUI.Helpers
{
    public class JsonNetValueProviderFactory : ValueProviderFactory
    {
        public JsonSerializerSettings Setting { get; set; }
        public JsonNetValueProviderFactory()
        {
            Setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
                Converters = { new ExpandoObjectConverter() }
            };
        }

        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext.Equals(null))
                throw new ArgumentNullException("controllerContext is NULL");

            if (controllerContext.HttpContext.Equals(null) ||
                controllerContext.HttpContext.Request.Equals(null) ||
                controllerContext.HttpContext.Request.ContentType.Equals(null))
                return null;

            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                return null;

            if (!controllerContext.HttpContext.Request.IsAjaxRequest())
                return null;

            using (StreamReader steamReader = new StreamReader(controllerContext.HttpContext.Request.InputStream))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(steamReader))
                {
                    if (!jsonReader.Read())
                        return null;

                    JsonSerializer jsonSerializer = JsonSerializer.Create(Setting);
                    Object jsonObject;
                    switch (jsonReader.TokenType)
                    {
                        case JsonToken.StartArray:
                            jsonObject = jsonSerializer.Deserialize<List<ExpandoObject>>(jsonReader);
                            break;
                        default:
                            jsonObject = jsonSerializer.Deserialize<ExpandoObject>(jsonReader);
                            break;
                    }
                    Dictionary<String, Object> backingStore = new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase);
                    addToBackingStore(backingStore, String.Empty, jsonObject);
                    return new DictionaryValueProvider<Object>(backingStore, CultureInfo.CurrentCulture);
                }
            }
        }

        private void addToBackingStore(Dictionary<String, Object> backingStore, String empty, Object jsonObject)
        {
            Dictionary<String, Object> dictionary = jsonObject as Dictionary<String, Object>;
            if (!dictionary.Equals(null))
            {
                foreach (KeyValuePair<String, Object> item in dictionary)
                    addToBackingStore(backingStore, makePropertyKey(empty, item.Key), jsonObject);

                return;
            }

            IList list = jsonObject as IList;
            if (!list.Equals(null))
            {
                for (int i = 0; i < list.Count; i++)
                    addToBackingStore(backingStore, makeArrayKey(empty, i), list[i]);

                return;
            }

            backingStore[empty] = jsonObject;
        }

        private static String makeArrayKey(String empty, Int32 i) =>
            empty + "[" + i.ToString(CultureInfo.InvariantCulture) + "]";

        private String makePropertyKey(String prefix, String key) =>
            (String.IsNullOrWhiteSpace(prefix) ? key : prefix + "." + prefix);
    }
}
