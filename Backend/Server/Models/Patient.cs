using BackendNetwork.Models;

namespace BackendNetwork.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string? Email { get; set; }

        public DateOnly? Birthdate { get; set; }

        public string? Gender { get; set; }
      
        public string? SocialSecurityNumber { get; set; }


        public string? Taxnumber { get; set; }

        public string? Religion { get; set; }

        public string? DriversLicense { get; set; }

        public string? MedicalInsuranceMemberNumber  { get; set; }

        public User? User { get; set; }
        
        public ICollection<Appointment>? Appointments { get; set; }

    }

}