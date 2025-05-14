namespace CrudApplication.DTOs
{
    public class CreateUserDto
    {
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } 
    }
}
 