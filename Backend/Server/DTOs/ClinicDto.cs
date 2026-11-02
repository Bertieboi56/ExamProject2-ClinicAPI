namespace BackendNetwork.DTOs
{
    
    public class ClinicDto
    {
        public int Id { get; set;}

        public string Name {get; set;}
    }
    public class CreateClinicDto
    {
        public string Name {get; set;}
    }
    public class UpdateClinicDto
    {
        public string Name {get; set;}
    }
}