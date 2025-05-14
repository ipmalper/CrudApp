using CrudApplication.Models;

namespace CrudApplication.Repositorio.Interfaces
{
    public interface IUsersRepository
    {   
        
        Users GetUser(int id);
        List<Users> GetUsers();
        Users AgregarUser(Users users);
        Users ActualizarUser(Users users);
        
        bool EmailExiste(string email);
        bool TelefonoExiste(string telefono);
        
        void BorrarUser(int id);
        void ActivarUser(int id);
        void DesactivarUser(int id);
    }
}
 