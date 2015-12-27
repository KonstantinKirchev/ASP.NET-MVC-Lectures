using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookmarks.Web.Infrastructure
{
    public abstract class BaseCacheService
    {
        protected T Get<T>(string cacheKey, Func<T> getItemcallback) where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemcallback();
                HttpContext.Current.Cache.Insert(cacheKey, item);
                return item;
            }

            return item;
        }
    }
}