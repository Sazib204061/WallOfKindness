using Microsoft.EntityFrameworkCore;
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
                    Title = g.Title,
                    Location = g.Location,
                    Description = g.Detail.Description,
                    GiftStatus = g.Detail.Status,
                    PrimaryImageUrl = g.Images.FirstOrDefault().ImageUrl,
                    ImageUrls = g.Images.Select(i => i.ImageUrl).ToList()
                })
                .ToListAsync();
        }

        public async Task<GiftVM> GetGiftByIdAsync(int id)
        {
            return await _db.Gifts
                .Where(g => g.Id == id)
                .Include(g => g.Detail)
                .Include(g => g.Images)
                .Select(g => new GiftVM
                {
                    Title = g.Title,
                    Location = g.Location,
                    Description = g.Detail.Description,
                    GiftStatus = g.Detail.Status,
                    PrimaryImageUrl = g.Images.FirstOrDefault().ImageUrl,
                    ImageUrls = g.Images.Select(i => i.ImageUrl).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
