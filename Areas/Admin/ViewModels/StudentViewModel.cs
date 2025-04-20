using System.ComponentModel.DataAnnotations;
using ComIT_eLearning.Models.Enums;

namespace ComIT_eLearning.Areas.Admin.Models
{
    public class StudentViewModel
    {
        public string? UserId { get; set; } // null = new user

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        public string? PreferredName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone number must be in the format XXX-XXX-XXXX")]
        public string? PhoneNumber { get; set; }


        // StudentProfile fields
        [Required]
        [MaxLength(20)]
        public string? StudentNumber { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

    }
}
