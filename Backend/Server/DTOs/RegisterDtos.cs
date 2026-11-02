using System.ComponentModel.DataAnnotations;

namespace BackendNetwork.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "First name is required")]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birthdate is required")]
        public DateOnly? Birthdate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Social security number is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Social security number must be at least 8 characters")]
        public string? SocialSecurityNumber { get; set; }

        [Required(ErrorMessage = "Tax number is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "taxnumber must be at least 8 characters")]
        public string? Taxnumber { get; set; }

        [Required(ErrorMessage = "Driver's licence is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "drivers licence must be at least 8 characters")]
        public string? DriversLicence { get; set; }

        [Required(ErrorMessage = "Religion is required")]
        public string? Religion { get; set; }

        [Required(ErrorMessage = "Medical insurance member number is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = " medical number must be at least 8 characters")]
        public string? MedicalInsuranceMemberNumber { get; set; }
    }
}

