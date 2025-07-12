using MomotarJhuri.Domain.Enums;

namespace MomotarJhuri.Web.Areas.GeneralUser.ViewModel
{
    public class ProfileVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PhotoLink { get; set; }
        public string Nationality { get; set; }
        public Gender Gender { get; set; }
    }
}