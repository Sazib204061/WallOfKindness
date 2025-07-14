using Microsoft.AspNetCore.Mvc;
using MomotarJhuri.Application.Gifts;

namespace MomotarJhuri.Web.Areas.GeneralUser.Controllers
{
    [Area("GeneralUser")]
    public class GiftController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGiftServices _giftServices;

        public GiftController(ILogger<HomeController> logger, IGiftServices giftServices)
        {
            _logger = logger;
            _giftServices = giftServices;
        }
        // GET: Gift
        public async Task<IActionResult> Index()
        {
            var gifts = await _giftServices.GetAllGiftsAsync();
            return View(gifts);
        }
    }
}
