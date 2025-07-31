using WallOfKindness.Domain.Entities;
using WallOfKindness.Domain.Enums;

namespace WallOfKindness.Application.Gifts
{
    public interface IGiftServices
    {
        Task<IEnumerable<GiftVM>> GetAllGiftsByUserIdAsync(string userId);
        Task<IEnumerable<GiftVM>> GetApprovedGiftsAsync();
        Task<IEnumerable<GiftVM>> GetPendingGiftsAsync();
        Task UpdateGiftStatusAsync(int giftId, PostStatus newStatus);
        Task<GiftVM> GetGiftFullDetailsById(int Id);
        Task CreateGiftWithDetailsAsync(Gift gift);
        Task DeleteGiftWithDelailsAsync(int Id);
        Task UpdateGiftWithDelailsAsync(Gift gift);


    }
}
