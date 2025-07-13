using MomotarJhuri.Domain.Entities;
using MomotarJhuri.Domain.Enums;

namespace MomotarJhuri.Application.Gifts
{
    public class GiftVM
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public GiftStatus GiftStatus { get; set; }
        public string PrimaryImageUrl{get; set;}
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
