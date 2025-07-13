namespace MomotarJhuri.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int GiftId { get; set; } //Foreign Key
        public Gift Gift { get; set; } //Navigation Property.
    }
}
