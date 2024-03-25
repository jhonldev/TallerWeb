namespace TallerWeb.Src.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;


        //Relaciones
        public int RoleId { get; set;}

        public Role Role { get; set; } = null!;
        
    }
}