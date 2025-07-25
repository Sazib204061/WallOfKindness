using MomotarJhuri.Domain.Entities;

namespace MomotarJhuri.Application.Gifts
{
    public interface IGiftServices
    {
        Task<IEnumerable<GiftVM>> GetAllGiftsAsync();
        Task<IEnumerable<GiftVM>> GetApprovedGiftsAsync();
        Task<GiftVM> GetGiftFullDetailsById(int Id);
        Task CreateGiftWithDetailsAsync(Gift gift);
        Task DeleteGiftWithDelailsAsync(int Id);
        Task UpdateGiftWithDelailsAsync(Gift gift);


    }
}
