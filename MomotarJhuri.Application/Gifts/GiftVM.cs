using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using MomotarJhuri.Domain.Entities;
using MomotarJhuri.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MomotarJhuri.Application.Gifts
{
    public class GiftVM
    {
        [ValidateNever]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public PostStatus? Status { get; set; }

        public string? Description { get; set; }
        public GiftStatus GiftStatus { get; set; }

        public string? PrimaryImageUrl{get; set;}
        public List<string>? ImageUrls { get; set; } = new List<string>();
        public List<IFormFile>? UploadedImages { get; set; } = new List<IFormFile>();

        public string? userId { get; set; }
        public string? UserFullName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }

    }
}
