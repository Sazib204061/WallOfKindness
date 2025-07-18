using Microsoft.AspNetCore.Mvc;
using MomotarJhuri.Application.Gifts;
using MomotarJhuri.Domain.Entities;

namespace MomotarJhuri.Web.Areas.GeneralUser.Controllers;

public class GiftController : Controller
{
    #region Fields

    private readonly ILogger<GiftController> _logger;
    private readonly IGiftServices _giftServices;
    private readonly IWebHostEnvironment _env;

    #endregion

    #region Ctor

    public GiftController(
        ILogger<GiftController> logger,
        IGiftServices giftServices,
        IWebHostEnvironment env)
    {
        _logger = logger;
        _giftServices = giftServices;
        _env = env;
    }
    #endregion

    #region Methods

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var gifts = await _giftServices.GetAllGiftsAsync();
        return View(gifts);
    }

    public async Task<IActionResult> GiftDetail(int Id)
    {
        var giftDetail = await _giftServices.GetGiftFullDetailsById(Id);
        return View(giftDetail);
    }

    

    [HttpGet]
    public IActionResult Create()
    {
        return View(new GiftVM());
    }

    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GiftVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            // 1. Create Gift entity
            var gift = new Gift
            {
                Title = model.Title,
                Location = model.Location,
            };

            // 2. Create GiftDetail entity
            gift.Detail = new GiftDetail
            {
                Description = model.Description,
                Status = model.GiftStatus,
                Gift = gift // Set navigation property
            };

            // 3. Process and create Image entities
            if (Request.Form.Files.Count > 0)
            {
                gift.Images = new List<Image>();
                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads", "gifts");

                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                foreach (var file in Request.Form.Files)
                {
                    if (file.Length > 0)
                    {
                        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine(uploadsPath, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        gift.Images.Add(new Image
                        {
                            ImageUrl = $"/uploads/gifts/{uniqueFileName}",
                            Gift = gift
                        });
                    }
                }
            }

            // 4. Save to database through service
            await _giftServices.CreateGiftWithDetailsAsync(gift);

            TempData["Success"] = "Gift created successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating gift");
            ModelState.AddModelError("", "An error occurred while creating the gift");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> MyAllGifts()
    {
        var gifts = await _giftServices.GetAllGiftsAsync();
        return View(gifts);
    }
    #endregion
}
