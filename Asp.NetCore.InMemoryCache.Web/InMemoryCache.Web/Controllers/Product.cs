using InMemoryCache.Web.Models;
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

        public IActionResult Index() { 
       
         
            if (!_memoryCache.TryGetValue("time", out string zamanCache))    // zaman keyinde data varmı diye bakılıyor
            {
                MemoryCacheEntryOptions option = new MemoryCacheEntryOptions();
                     /* option.AbsoluteExpiration = DateTime.Now.AddSeconds(30);*/     // 30 saniye sonra hafızadan siler

                option.SlidingExpiration= TimeSpan.FromSeconds(10);               //Dataya eriştiğimiz sürece 10 saniye ömrünü uzatacak, 10 saniye erişmezsek cacheden silinecek

                option.Priority = CacheItemPriority.High;//hafıza dolduğunda Time Key deki veriler benim için önemli bunu silme
                option.Priority = CacheItemPriority.Low;//hafıza dolduğunda Time Key deki veriler benim için önemli değil bunu sil
               

                _memoryCache.Set<string>("time", DateTime.Now.ToString(),option);


                Products p = new Products { Id=1 , Name="Pencil",Price=100};
                _memoryCache.Set<Products>("product:1", p);
            }
            return View();
        }
        public IActionResult Show()
        {
            //_memoryCache.Remove("zaman");        //Keye ait cache kaldırır.

            //_memoryCache.GetOrCreate<string>("time", entry =>
            //{
            //    return DateTime.Now.ToString();
            //});           //  Time Keyinde sahip değeri almaya çalışır eğer yoksa kendisi oluşturur.

            _memoryCache.TryGetValue("time", out string zamanCache);
            ViewBag.Time = zamanCache;
            _memoryCache.Get<Products>("product:1");

            /*ViewBag.zaman=_memoryCache.Get<string>("time"); *///key ile alınacak datayı seçtik
            return View();
        }
    }
}
