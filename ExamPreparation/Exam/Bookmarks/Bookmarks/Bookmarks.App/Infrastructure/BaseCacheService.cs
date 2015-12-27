namespace Bookmarks.App.Infrastructure
{
    using System;
    using System.Web;

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