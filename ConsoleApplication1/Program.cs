using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Data;
using SpaUserControl.Infraestructure.Repositories;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User(
                "Lysandro",
                "lysandroc@gmail.com");
            
            user.SetPassword("senha123","senha123");
            user.Validate();

            Console.WriteLine(user.Id);

            using (IUserRepository userRep = new UserRepository(new AppDataContext()))
            {
                userRep.Create(user);
            }

            using (IUserRepository userRep = new UserRepository(new AppDataContext()))
            {
                userRep.Update(user);
            }

            using (IUserRepository userRep = new UserRepository(new AppDataContext()))
            {
                var us = userRep.Get("lysandroc@gmail.com");
                Console.WriteLine(us.Email +" - "+ us.Name);
                Console.WriteLine(user.Id);
            }

            Console.ReadKey();
        }
    }
}
