using MomotarJhuri.Domain.Enums;
namespace MomotarJhuri.Domain.Entities
{
    internal class GiftDetail
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public GiftStatus Status { get; set; }
        public int ImageId { get; set; }
    }
}


