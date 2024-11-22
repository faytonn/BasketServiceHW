using Allup.Application.Services.Abstracts;
using Allup.Application.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Allup.MVC.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddBasket(int productId)
        {
            var basket = Request.Cookies["basket"];
            var basketCookieViewModels = new List<BasketCookieViewModel>();

            if (string.IsNullOrEmpty(basket))
            {
                basketCookieViewModels.Add(new BasketCookieViewModel
                {
                    Count = 1,
                    ProductId = productId
                });
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketCookieViewModels));
            }

            return RedirectToAction("Index","Home");
        }
    }
}
