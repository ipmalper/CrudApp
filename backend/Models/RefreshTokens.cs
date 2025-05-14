namespace CrudApplication.Models
{
    public class RefreshTokens
        
    {
        public int Id { get; set; }

        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool Revoked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

    }
}
  