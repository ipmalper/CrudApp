using System.Data;
using CrudApplication.Models;
using CrudApplication.Repositorio.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CrudApplication.Repositorio.Implementaciones
{
    public class RefreshTokensRepository : IRefreshTokensRepository
    {
        private readonly IDbConnection _conexion;
        public RefreshTokensRepository(IConfiguration configuration)
        {
            _conexion = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocal"));
        }

        public bool ExisteUsuario(string email, string userName)
        {
            var sql = "SELECT COUNT(*) FROM RefreshTokens WHERE Email = @Email OR UserName = @UserName";
            int count = _conexion.ExecuteScalar<int>(sql, new { Email = email, UserName = userName });
            return count > 0;
        }

        public RefreshTokens LoginUsuario(string email, string password, string userName)
        {
              using (var db = _conexion)
            {
                var refreshTokens = db.QueryFirstOrDefault<RefreshTokens>(
                    "SELECT * FROM RefreshTokens WHERE Email = @Email AND UserName = @UserName",
                    new { Email = email, UserName = userName }
                );

                if (refreshTokens != null && BCrypt.Net.BCrypt.Verify(password, refreshTokens.Password))
                {    
                    refreshTokens.Token = Guid.NewGuid().ToString(); // Token aleatorio
                    refreshTokens.ExpiresAt = DateTime.Now.AddMinutes(30); // Caduca en 30 min

                    db.Execute("UPDATE RefreshTokens SET Token = @Token, ExpiresAt = @ExpiresAt WHERE Id = @Id",
                        new { refreshTokens.Token, refreshTokens.ExpiresAt, refreshTokens.Id });
                }

                return refreshTokens;
            }

        }

        public RefreshTokens ObtenerPorToken(string token)
        {
            using (var db = _conexion)
            {
                return db.QueryFirstOrDefault<RefreshTokens>(
                    "SELECT * FROM RefreshTokens WHERE Token = @Token",
                    new { Token = token });
            }
        }

        public bool RegistroUsuario(string email, string password, string userName)
        {
            if (ExisteUsuario(email, userName))
            {
                throw new Exception("El correo electrónico o el nombre de usuario ya están en uso.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var sql = "INSERT INTO RefreshTokens (Email, Password, UserName, CreatedAt) " +
                      "VALUES (@Email, @Password, @UserName, @CreatedAt)";

            int rowsAffected = _conexion.Execute(sql, new
            {
                Email = email,
                Password = passwordHash,
                UserName = userName, 
                CreatedAt = DateTime.Now
            });

            return rowsAffected > 0;
        }

        
    }
}
