using Google.Protobuf.WellKnownTypes;

namespace BackendNetwork.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Duration {get; set;}

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set;}

        public int CategoryId { get; set;}

        public Category? Category {get; set;}
    }
}