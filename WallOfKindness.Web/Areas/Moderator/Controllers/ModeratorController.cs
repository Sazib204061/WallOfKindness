using Microsoft.AspNetCore.Mvc;
using WallOfKindness.Application.Gifts;
using WallOfKindness.Domain.Enums;
using WallOfKindness.Web.Areas.GeneralUser.Controllers;

namespace WallOfKindness.Web.Areas.Moderator.Controllers
{
    public class ModeratorController : Controller
    {
        private readonly ILogger<GiftController> _logger;
        private readonly IGiftServices _giftServices;
        private readonly IWebHostEnvironment _env;

        public ModeratorController(
            ILogger<GiftController> logger,
            IGiftServices giftServices,
            IWebHostEnvironment env)
        {
            _logger = logger;
            _giftServices = giftServices;
            _env = env;
        }

       
        [HttpGet]

        public async Task<IActionResult> Dashboard()
        {
            var PendingGifts = await _giftServices.GetPendingGiftsAsync();
            return View(PendingGifts);
        }


        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            await _giftServices.UpdateGiftStatusAsync(id, PostStatus.Approved);
            TempData["Success"] = "Gift approved successfully!";
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int Id)
        {
            await _giftServices.DeleteGiftWithDelailsAsync(Id);
            TempData["Success"] = "Gift deleted successfully!";
            _logger.LogError("Gift Deleted Successfully");
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
