using Tp8.Interfaces;

namespace Tp8.Repository
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool Login(string username, string password)
        {
            return true;
        }
        void Logout()
        {
            
        }
        bool IsAuthenticated()
        {
            return true;
        }
        // Verifica si el usuario actual tiene el rol requerido (ej. "Administrador").
        bool HasAccessLevel(string requiredAccessLevel)
        {
            return true;
        }
    }
}