namespace BackendNetwork.DTOs
{
    
    public class SpecialityDto
    {
        public int Id { get; set;}

        public string Name {get; set;}
    }

    public class UpdateSpecialityDto
    {
        public string Name {get; set;}
    }

    public class CreateSpecialityDto
    {
        public string Name {get; set;}
    }
}