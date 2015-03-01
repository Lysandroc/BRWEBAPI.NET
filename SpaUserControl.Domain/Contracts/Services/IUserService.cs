using System;
using SpaUserControl.Domain.Models;

namespace SpaUserControl.Domain.Contracts.Services
{
    public interface IUserService : IDisposable
    {
        User Authenticate(string email, string password);

        User GetByEmail(string email);

        void Registrer(string name, string email, string password, string confirmpassword);

        void ChangeInformation(string email, string name);

        void ChangePassword(string email, string password, string newPassword, string confirmNewPassword);

        string ResetPassword(string email);
    }
}
