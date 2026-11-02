namespace BackendNetwork.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public Role? Role { get; set; }

        public int PatientId { get; set; }

        public Patient? Patient { get; set; }
    }

}