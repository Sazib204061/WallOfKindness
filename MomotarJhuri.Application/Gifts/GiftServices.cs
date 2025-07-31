using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WallOfKindness.Domain.Entities;
using WallOfKindness.Domain.Enums;
using WallOfKindness.Infractructure.Data;

namespace WallOfKindness.Application.Gifts
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

        public async Task<IEnumerable<GiftVM>> GetAllGiftsByUserIdAsync(string userId)
        {
            return await _db.Gifts
                .Where(g => g.UserId == userId)  // Filter by user ID
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

        public async Task<IEnumerable<GiftVM>> GetPendingGiftsAsync()
        {
            return await _db.Gifts
                .Where(g => g.Status == PostStatus.Pending) // Filter by Approved status
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

        public async Task UpdateGiftStatusAsync(int giftId, PostStatus newStatus)
        {
            var gift = await _db.Gifts.FindAsync(giftId);
            if (gift == null)
                throw new Exception("Gift not found.");

            gift.Status = newStatus; // Update status
            await _db.SaveChangesAsync();
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

        public async Task UpdateGiftWithDelailsAsync(Gift gift)
        {
            var existingGift = await _db.Gifts
                .Include(g => g.Detail)
                .Include(g => g.Images)
                .FirstOrDefaultAsync(g => g.Id == gift.Id);

            if (existingGift == null)
            {
                throw new Exception("Gift not found.");
            }

            // Update main Gift fields
            existingGift.Title = gift.Title;
            existingGift.Location = gift.Location;
            existingGift.Status = gift.Status;

            // Update GiftDetail
            if (existingGift.Detail != null)
            {
                existingGift.Detail.Description = gift.Detail.Description;
                existingGift.Detail.Status = gift.Detail.Status;
            }
            else
            {
                existingGift.Detail = new GiftDetail
                {
                    Description = gift.Detail.Description,
                    Status = gift.Detail.Status,
                    Gift = existingGift
                };
            }

            // 🧹 Step: Remove old images from file system + database
            if (existingGift.Images != null && existingGift.Images.Any())
            {
                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads", "gifts");

                foreach (var oldImage in existingGift.Images)
                {
                    var filePath = Path.Combine(uploadsPath, Path.GetFileName(oldImage.ImageUrl));
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath); // ✅ Delete from folder
                    }
                }

                _db.Images.RemoveRange(existingGift.Images); // ✅ Remove from DB
            }

            // ➕ Step: Add new images (from controller)
            if (gift.Images != null && gift.Images.Any())
            {
                existingGift.Images = new List<Image>();
                foreach (var img in gift.Images)
                {
                    existingGift.Images.Add(new Image
                    {
                        ImageUrl = img.ImageUrl,
                        Gift = existingGift
                    });
                }
            }

            _db.Gifts.Update(existingGift);
            await _db.SaveChangesAsync();
        }

    }
}
