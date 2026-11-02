namespace BackendNetwork.Models
{
    public class Speciality
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Doctor>? Doctors { get; set; }


    
    }
}