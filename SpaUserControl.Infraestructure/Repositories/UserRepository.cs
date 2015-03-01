﻿using System;
using System.Linq;
using SpaUserControl.Domain.Models;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Infraestructure.Data;
using System.Data.Entity;

namespace SpaUserControl.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDataContext _context = new AppDataContext();

        public User Get(string email)
        {
            return _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public User Get(Guid id)
        {
            return _context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Entry<User>(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
