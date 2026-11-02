using System.ComponentModel.DataAnnotations;

namespace BackendNetwork.DTOs
{

    public class CreateAppointmentDto
    {
        public DateTime Date {get; set;}

        public int Duration { get; set;}

        public int DoctorId {get; set;}

        public int CategoryId {get; set;}

         public string? Firstname { get; set; }
         public string? Lastname { get; set; }

         public DateOnly? Birthdate { get; set; }
      
    }
    public class AppointmentDto
    {
        public int Id {get; set;}

        public DateTime Date {get; set;}


        public int PatientId {get; set;}

        public int Duration {get; set;}

        public int DoctorId {get; set;}

        public int CategoryId {get; set;}

        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
    public class UpdateAppointmentDto
    {
    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }

     
    public int DoctorId { get; set; }

    [Range(1, 60, ErrorMessage = "Duration must be between 1 and 60 minutes")]
    public int Duration { get; set; }

    
    public int CategoryId { get; set; }
    }

    public class GetAppointmentDto
    {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public int DoctorId { get; set; }
    public int CategoryId { get; set; }

    public string DoctorName { get; set; }
    public string ClinicName { get; set; }
    public string CategoryName { get; set; }
    }
    
}