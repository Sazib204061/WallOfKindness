using MomotarJhuri.Domain.Entities;

namespace MomotarJhuri.Application.Gifts
{
    public interface IGiftServices
    {
        Task<IEnumerable<GiftVM>> GetAllGiftsAsync();
        Task<GiftVM> GetGiftFullDetailsById(int Id);
        Task CreateGiftWithDetailsAsync(Gift gift);

    }
}
