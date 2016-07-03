using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Extentions
{
    public static class CacheManager
    {
        public static void CacheInsert(this HttpContextBase httpContext, String key, Object data, Int32 durationMinutes)
        {
            if (data.Equals(null))
                return;
            httpContext.Cache.Add(key,
                                                    data,
                                                    null,
                                                    DateTime.Now.AddMinutes(durationMinutes),
                                                    TimeSpan.Zero,
                                                    System.Web.Caching.CacheItemPriority.AboveNormal,
                                                    null);
        }

        public static T CacheRead<T>(this HttpContextBase httpContext, String key)
        {
            Object data = httpContext.Cache[key];
            if (data != null)
                return (T)data;
            return default(T);
        }

        public static void InvalidateCache(this HttpContextBase httpContext, String key)
        {
            httpContext.Cache.Remove(key);
        }

        public static void InvalidateOutPutCache(String url)
        {
            HttpResponse.RemoveOutputCacheItem(url);
        }

        public static void InvalidateChildActionsCache()
        {
            OutputCacheAttribute.ChildActionCache = new System.Runtime.Caching.MemoryCache(Guid.NewGuid().ToString());
        }
    }
}
