using System.ComponentModel.DataAnnotations;

namespace MomotarJhuri.Web.Areas.GeneralUser.ViewModel
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
