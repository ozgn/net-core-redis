using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Redis.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _distributedCache;

        public HomeController (IDistributedCache distributedCache)
        {
            _distributedCache=distributedCache;
        }
        public IActionResult Index()
        {
            var cachedKey="Time";
            var time=_distributedCache.GetString(cachedKey);
            if(!string.IsNullOrEmpty(time))
                return Content("From Cache: "+time);
            else
            {
                time=DateTime.Now.ToString();
                _distributedCache.SetString(cachedKey,time);
                return Content("Added To Cache: "+time);
            }
        }
    }
}
