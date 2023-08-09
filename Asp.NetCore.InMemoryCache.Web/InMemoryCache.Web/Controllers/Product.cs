using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCache.Web.Controllers
{
    public class Product : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public Product(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            _memoryCache.Set<string>("time", DateTime.Now.ToString());//Cachelenecek verimizi ve bir key girdik.
            return View();
        }
        public IActionResult Show()
        {
            ViewBag.zaman=_memoryCache.Get<string>("time"); //key ile alınacak datayı seçtik
            return View();
        }
    }
}
