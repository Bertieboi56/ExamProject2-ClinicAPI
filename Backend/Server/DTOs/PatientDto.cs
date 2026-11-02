using System.ComponentModel.DataAnnotations;
namespace BackendNetwork.DTOs
{
    public class CreatePatientDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be at least 2-50 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be at least  2-50 characters")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        public DateOnly? Birthdate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
    }
public class PatientDto
    {
        public int Id {get; set;}

        public string Firstname {get; set;}

        public string Lastname {get; set;}

        public string Email { get; set;}

        public DateOnly? Birthdate {get; set;}

        public string? Gender { get; set; }

        public string? SocialSecurityNumber { get; set;}

        public string? TaxNumber {get; set;}

        public string? Religion {get; set;}

        public string? DriversLicence {get; set;}
        public string? MedicalInsuranceMemberNumber { get; set; }
    }

    public class UpdatePatientDto
    {


      [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be at least 2-50 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be at least  2-50 characters")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        public DateOnly? Birthdate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Social security number must be at least 8 characters")]
        public string? SocialSecurityNumber { get; set;}
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Tax number must be at least 8 characters")]
        public string? TaxNumber {get; set;}
        
        public string? Religion {get; set;}
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Drivers licence must be at least 8 characters")]
        public string? DriversLicence {get; set;}
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Medical insurance member number must be at least 8 characters")]
        public string? MedicalInsuranceMemberNumber { get; set; }
    }



}