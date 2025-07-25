using MomotarJhuri.Domain.Enums;
namespace MomotarJhuri.Domain.Entities
{
    public class GiftDetail
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public GiftStatus Status { get; set; }  //this is gift availabe or not available status.
        public int GiftId {  get; set; } //Foreign Key from Gift table
        public Gift Gift { get; set; } //Navigation property.
    }
}


