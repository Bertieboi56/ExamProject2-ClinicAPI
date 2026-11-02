namespace BackendNetwork.DTOs
{
    
    public class DoctorDto
    {
        public int Id { get; set;}

        public string Firstname {get; set;}

        public string Lastname {get; set;}

        public int SpecialityId {get; set;}

        public int ClinicId {get; set;}
    }

    public class CreateDoctorDto
    {
        public string Firstname {get; set;}

        public string Lastname {get; set;}

        public int SpecialityId {get; set;}

        public int ClinicId {get; set;}
    }

    public class UpdateDoctorDto
    {
         public string Firstname {get; set;}

        public string Lastname {get; set;}

        public int SpecialityId {get; set;}

        public int ClinicId {get; set;}
    }

    public class SearchDoctorDto
    {
    public string? FullName { get; set; }
    public string? ClinicName { get; set; }
    public string? SpecialityName { get; set; }
    }

 }