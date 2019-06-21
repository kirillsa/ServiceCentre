using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using BLL;
using BLL.DTO;
using BLL.Infrastructure;
using BLL_Console.infrastructure;
using BLL.Interfaces;

namespace BLL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Go();
            Console.ReadLine();
        }
        static void Go()
        {
            var newUser = new UserDTO()
            {
                Email = "gfdgd@a.a",
                Password = "123456",
                Name = "user4",
                Role = "user"
            };

            var roles = new List<string>() { "manager", "admin" };
            var kernel = new StandardKernel(new BLL_DI(), new BllDependencyInjection());
            var core = kernel.Get<IMainService>();


            foreach (var item in core.UserServices.GetAllUsers())
            {
                Console.WriteLine($"{item.Id} - {item.UserName}");
            }
            core.UserServices.SetInitialData(newUser, roles);
            var users = core.UserServices.GetAllUsers();
            var user = users.FirstOrDefault();
            user.Name = "111";
            user.UserName = "111";
            core.UserServices.EditUser(user);
            core.Save();
            
            foreach (var item in core.UserServices.GetAllUsers())
            {
                Console.WriteLine($"{item.Id} - {item.UserName}");
            }
            
        }
    }
}
