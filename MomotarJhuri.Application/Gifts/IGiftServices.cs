using MomotarJhuri.Domain.Entities;
using MomotarJhuri.Domain.Enums;

namespace MomotarJhuri.Application.Gifts
{
    public interface IGiftServices
    {
        Task<IEnumerable<GiftVM>> GetAllGiftsAsync();
        Task<IEnumerable<GiftVM>> GetApprovedGiftsAsync();
        Task<IEnumerable<GiftVM>> GetPendingGiftsAsync();
        Task UpdateGiftStatusAsync(int giftId, PostStatus newStatus);
        Task<GiftVM> GetGiftFullDetailsById(int Id);
        Task CreateGiftWithDetailsAsync(Gift gift);
        Task DeleteGiftWithDelailsAsync(int Id);

    }
}
