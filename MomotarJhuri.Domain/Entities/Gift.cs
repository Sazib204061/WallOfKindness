namespace MomotarJhuri.Domain.Entities
{
    public class Gift
    {
        int Id { get; set; }
        public string? Location { get; set; }
        int GiftDetailId { get; set; }
        int UserId { get; set; }

    }
}
