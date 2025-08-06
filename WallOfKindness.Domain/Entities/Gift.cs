using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WallOfKindness.Domain.Enums;

namespace WallOfKindness.Domain.Entities
{
    public class Gift
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public PostStatus Status { get; set; }

        public string UserId { get; set; } //Foreign key form ApplicationUser table
        public ApplicationUser User { get; set; }
        public GiftDetail Detail { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
