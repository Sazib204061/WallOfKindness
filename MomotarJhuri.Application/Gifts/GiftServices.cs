using Microsoft.EntityFrameworkCore;
using MomotarJhuri.Domain.Entities;
using MomotarJhuri.Domain.Enums;
using MomotarJhuri.Infractructure.Data;

namespace MomotarJhuri.Application.Gifts
{
    public class GiftServices : IGiftServices
    {
        private readonly ApplicationDbContext _db;

        public GiftServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<GiftVM>> GetAllGiftsAsync()
        {
            return await _db.Gifts
                .Include(g=>g.Detail)
                .Include(g=>g.Images)
                .Select(g=> new GiftVM
                {
                    Id = g.Id,
                    Title = g.Title,
                    Location = g.Location,
                    Description = g.Detail.Description,
                    GiftStatus = g.Detail.Status,
                    PrimaryImageUrl = g.Images.FirstOrDefault().ImageUrl,
                    ImageUrls = g.Images.Select(i => i.ImageUrl).ToList()
                }).ToListAsync();
        }

        public async Task<GiftVM> GetGiftFullDetailsById(int Id)
        {
            var gift = await _db.Gifts
            .Include(g => g.Detail)
            .Include(g => g.Images)
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
                ImageUrls = gift.Images.Select(i => i.ImageUrl).ToList()
            };

        }

        public async Task CreateGiftWithDetailsAsync(Gift gift)
        {
            await _db.AddAsync(gift);
            await _db.SaveChangesAsync();
        }
    }
}
