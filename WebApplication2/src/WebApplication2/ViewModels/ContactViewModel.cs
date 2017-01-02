using System.ComponentModel.DataAnnotations;

namespace Heinbo.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(4096, MinimumLength = 10)]
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}
