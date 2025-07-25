using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MomotarJhuri.Domain.Entities;
using MomotarJhuri.Domain.Enums;
using MomotarJhuri.Infractructure.Data;

namespace MomotarJhuri.Application.Gifts
{
    public class GiftServices : IGiftServices
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        

        public GiftServices(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IEnumerable<GiftVM>> GetAllGiftsAsync()
        {
            return await _db.Gifts
                .Include(g => g.Detail)
                .Include(g => g.Images)
                .Select(g => new GiftVM
                {
                    Id = g.Id,
                    Title = g.Title,
                    Location = g.Location,
                    Status = g.Status,
                    Description = g.Detail.Description,
                    GiftStatus = g.Detail.Status,
                    PrimaryImageUrl = g.Images.FirstOrDefault().ImageUrl,
                    ImageUrls = g.Images.Select(i => i.ImageUrl).ToList()
                }).ToListAsync();
        }


        public async Task<IEnumerable<GiftVM>> GetApprovedGiftsAsync()
        {
            return await _db.Gifts
                .Where(g => g.Status == PostStatus.Approved) // Filter by Approved status
                .Include(g => g.Detail)
                .Include(g => g.Images)
                .Include(g => g.User) // Optional: Include user details if needed
                .Select(g => new GiftVM
                {
                    Id = g.Id,
                    Title = g.Title,
                    Location = g.Location,
                    Status = g.Status,
                    Description = g.Detail.Description,
                    GiftStatus = g.Detail.Status,
                    PrimaryImageUrl = g.Images.FirstOrDefault().ImageUrl,
                    ImageUrls = g.Images.Select(i => i.ImageUrl).ToList(),
                    UserFullName = g.User.FullName, // Optional
                    UserEmail = g.User.Email,      // Optional
                    UserPhoneNumber = g.User.PhoneNumber // Optional
                })
                .ToListAsync();
        }



        public async Task<GiftVM> GetGiftFullDetailsById(int Id)
        {
            var gift = await _db.Gifts
            .Include(g => g.Detail)
            .Include(g => g.Images)
            .Include(g => g.User)
            .FirstOrDefaultAsync(g => g.Id == Id);

            if (gift == null)
                return null;

            return new GiftVM
            {
                Id = gift.Id,
                Title = gift.Title,
                Location = gift.Location,
                Description = gift.Detail?.Description,
                GiftStatus = gift.Detail?.Status ?? GiftStatus.InProgress,
                PrimaryImageUrl = gift.Images.FirstOrDefault()?.ImageUrl,
                ImageUrls = gift.Images.Select(i => i.ImageUrl).ToList(),
                UserFullName = gift.User.FullName,
                UserEmail = gift.User.Email,
                UserPhoneNumber = gift.User.PhoneNumber
            };

        }

        public async Task CreateGiftWithDetailsAsync(Gift gift)
        {
            await _db.AddAsync(gift);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteGiftWithDelailsAsync(int Id)
        {
            try
            {
                var gift = await _db.Gifts
                    .Include(g => g.Detail)
                    .Include(g => g.Images)
                    .FirstOrDefaultAsync(g => g.Id == Id);
                if (gift == null)
                {
                    throw new Exception("Gift not found");
                }

                await DeleteGiftImages(gift);  //Delete associated images form filesystem.


                _db.Gifts.Remove(gift);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error deleting gift with ID {Id}: {ex.Message}");
            }
        }

        private async Task DeleteGiftImages(Gift gift)
        {
            try
            {
                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads", "gifts");
                //if(!string.IsNullOrEmpty(gift.PrimaryImageUrl))
                //{
                //    var primaryImagePath = Path.Combine(uploadsPath, Path.GetFileName(gift.PrimaryImageUrl);
                //    if (File.Exists(primaryImagePath))
                //    {
                //        File.Delete(primaryImagePath);
                //    }
                //}

                if(gift.Images!=null && gift.Images.Any())
                {
                    foreach(var image in gift.Images)
                    {
                        var imagePath = Path.Combine(uploadsPath, Path.GetFileName(image.ImageUrl));
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Error deleting images for gift with ID {gift.Id}: {ex.Message}");
            }
        }
    }
}
