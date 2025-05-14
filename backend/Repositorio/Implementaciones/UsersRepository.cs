using CrudApplication.Models;
using CrudApplication.Repositorio.Interfaces;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Identity;
using System;
using CrudApplication.Helpers;

namespace CrudApplication.Repositorio.Implementaciones
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbConnection _bd;

        public UsersRepository(IConfiguration configuration)
        {

            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocal"));
        }

        public void ActivarUser(int id)
        {
            var sql = "UPDATE Users SET Activo = 1 WHERE UserId=@UserId";
            _bd.Execute(sql, new { UserId  = id });
        }

        public Users ActualizarUser(Users users)
        {
            var sql = "UPDATE  [Users] SET Nombres=@Nombres, ApellidoPaterno=@ApellidoPaterno, ApellidoMaterno=@ApellidoMaterno, Telefono=@Telefono, Email=@Email, PasswordHash=@PasswordHash, FechaCreacion=@FechaCreacion, Activo=@Activo"
                + " WHERE UserId=@UserId";
            _bd.Execute(sql, users);
            return users; 
        }

        public Users AgregarUser(Users users)
        {
            users.FechaCreacion = DateTime.Now;
            var sql = "INSERT INTO [USERS](Nombres, ApellidoPaterno, ApellidoMaterno, Telefono, Email, PasswordHash, FechaCreacion, Activo)"
                + "VALUES(@Nombres, @ApellidoPaterno, @ApellidoMaterno, @Telefono, @Email, @PasswordHash, @FechaCreacion, @Activo)"
                + " SELECT CAST(SCOPE_IDENTITY() as int);";

            users.PasswordHash = PasswordHelper.HashPassword(users.PasswordHash);
            var id = _bd.Query<int>(sql, new
            {
                users.Nombres,
                users.ApellidoPaterno,
                users.ApellidoMaterno,
                users.Telefono,
                users.Email,
                users.PasswordHash,
                users.FechaCreacion,
                users.Activo
            }).Single();
            users.UserId = id;
            return users;
        }

        public void BorrarUser(int id)
        {
            var sql = "DELETE FROM [Users] WHERE UserId=@UserId";
            _bd.Execute(sql, new { UserId = id });
        }

        public void DesactivarUser(int id)
        {
            var sql = "UPDATE Users SET Activo=0 WHERE UserId=@UserId";
            _bd.Execute(sql, new { UserId = id });
        }

        public bool EmailExiste(string email)
        {
            var sql = "SELECT COUNT(1) FROM [Users] WHERE Email=@Email";
            var count = _bd.ExecuteScalar<int>(sql, new { Email = email });
            return count > 0;
        }

        public Users GetUser(int id)
        {
            var sql = "SELECT * FROM [Users] WHERE  UserId=@UserId";
            return _bd.Query<Users>(sql, new { @UserId = id}).Single();
        }

        public List<Users> GetUsers()
        {
            var sql = "SELECT * FROM [Users] WHERE Activo=1";
            return _bd.Query<Users>(sql).ToList();
        }

        public bool TelefonoExiste(string telefono)
        {
            var sql = "SELECT COUNT(1) FROM [Users] WHERE Telefono = @Telefono";
            var count = _bd.ExecuteScalar<int>(sql, new { Telefono = telefono });
            return count > 0;
        }
    }
}
