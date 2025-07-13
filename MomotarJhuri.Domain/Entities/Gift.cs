using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MomotarJhuri.Domain.Entities
{
    public class Gift
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public GiftDetail Detail { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
