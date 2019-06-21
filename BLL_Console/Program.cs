using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.DTO;

namespace BLL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Go();
            Console.ReadLine();
        }
        static async void Go()
        {
            var newUser = new UserDTO()
            {
                Email = "admin@a.a",
                Password = "123456",
                Name = "user2",
                Role = "manager"
            };

            var roles = new List<string>() { "manager", "admin" };

            var core = new CoreServices();


            //foreach (var item in core.UserServices.GetAllUsers())
            //{
            //    Console.WriteLine($"{item.Id} - {item.UserName}");
            //}
            core.UserServices.SetInitialData(newUser, roles);

            //core.UserServices.Create(newUser);
            core.SaveAsync();


            //foreach (var item in core.UserServices.GetAllUsers())
            //{
            //    Console.WriteLine($"{item.Id} - {item.UserName}");
            //}
            
        }
    }
}
