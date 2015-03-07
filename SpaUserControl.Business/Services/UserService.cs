using System;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Contracts.Services;
using  SpaUserControl.Domain.Models;
using SpaUserControl.Common.Resource;
using SpaUserControl.Common.Validation;


namespace SpaUserControl.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            this._repository = repository;
        }
        
        public User Authenticate(string email, string password)
        {
            var user = GetByEmail(email);
            if (user.Password != PasswordAssertionConcern.Encrypt(password))
                throw new Exception(Errors.InvalidCredentials);
            
            return user;
        }

        public User GetByEmail(string email)
        {
            var user = _repository.Get(email);
            if (user == null)
            {
                throw new Exception(Errors.UserNotFound);
            }
            return user;
        }

        public void Registrer(string name, string email, string password, string confirmpassword)
        {
            var hasUser = _repository.Get(email);
            if (hasUser != null)
                throw new Exception(Errors.DuplicateEmail);

            var user = new User(name, email);
            user.SetPassword(password,confirmpassword);
            user.Validate();
            
            _repository.Create(user);
        }

        public void ChangeInformation(string email, string name)
        {
            var user = GetByEmail(email);

            user.ChangeName(name);
            user.Validate();
            _repository.Update(user);
        }

        public void ChangePassword(string email, string password, string newPassword, string confirmNewPassword)
        {
            var user = Authenticate(email, password);
            user.SetPassword(newPassword,confirmNewPassword);
            user.Validate();

            _repository.Update(user);
        }

        public string ResetPassword(string email)
        {
            var user = GetByEmail(email);
            var passsword = user.ResetPassword();
            user.Validate();
            
            _repository.Update(user);
            return passsword;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
