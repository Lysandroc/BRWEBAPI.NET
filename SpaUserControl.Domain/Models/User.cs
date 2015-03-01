
using System;
using SpaUserControl.Common.Resource;
using SpaUserControl.Common.Validation;

namespace SpaUserControl.Domain.Models
{
    public class User
    {
        #region Ctor

        protected User() 
        {
            //Entity Framework 
        }
        public User(string name, string email)
        {
            this.Id = new Guid();
            this.Name = name;
            this.Email = email;
        }
        #endregion 

        #region Properties
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }
        #endregion

        #region Methods
        public void SetPassword(string password, string confirmpassword)
        {
           AssertionConcern.AssertArgumentNotNull(password,Errors.InvalidPassword);
           AssertionConcern.AssertArgumentNotNull(confirmpassword, Errors.InvalidPasswordConfirmation);
           AssertionConcern.AssertArgumentEquals(password, confirmpassword, Errors.InvalidPasswordConfirmation);
           AssertionConcern.AssertArgumentLength(password,6,20,  Errors.PasswordDoNotMatch);
           
            this.Password = PasswordAssertionConcern.Encrypt(password);
        }

        public string ResetPassword()
        {
            string password = Guid.NewGuid().ToString().Substring(0, 8);
            this.Password = PasswordAssertionConcern.Encrypt(password);

            return password;
        }

        public void ChangeName(string name)
        {
            this.Name = name;
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentLength(this.Name, 3, 60, Errors.InvalidUserName);
            EmailAssertionConcern.AssertIsValid(this.Email);
            PasswordAssertionConcern.AssertIsValid(this.Password);
        }
        #endregion
    }
}
