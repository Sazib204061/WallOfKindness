namespace MomotarJhuri.Application.Gifts
{
    public interface IGiftServices
    {
        Task<IEnumerable<GiftVM>> GetAllGiftsAsync();
        Task<GiftVM> GetGiftByIdAsync(int id);
    }
}
