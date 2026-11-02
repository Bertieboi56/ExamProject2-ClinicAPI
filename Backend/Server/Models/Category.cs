namespace BackendNetwork.Models
{
    public class Category
    {
        public int id { get; set; }
        public string Name { get; set; }

        public ICollection<Appointment>? Appointments {get; set;}
    }
}