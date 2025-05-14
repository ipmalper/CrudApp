namespace CrudApplication.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public string PasswordHash { get; set; }
    }
}
