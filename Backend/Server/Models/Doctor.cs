namespace BackendNetwork.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int SpecialityId { get; set; }

        public Speciality? Speciality { get; set; }

        public int ClinicId { get; set; }

        public Clinic? Clinic { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}