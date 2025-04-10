using System.ComponentModel.DataAnnotations;
using ComIT_eLearning.Models.Enums;

namespace ComIT_eLearning.Areas.Admin.ViewModels
{
    public class CreateTeacherViewModel
    {
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


        // TeacherProfile fields
        [Required]
        [MaxLength(20)]
        public string? EmployeeNumber { get; set; }

        [Required]
        public PositionType Position { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        [MaxLength(100)]
        public string? OfficeLocation { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Url]
        public string? WebsiteUrl { get; set; }
    }
}
