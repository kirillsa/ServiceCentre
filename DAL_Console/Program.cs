using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DBContext.Models;

namespace DAL_Console
{

    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Go();
            Console.ReadLine();
        }

        static async void Go()
        {
            var uow = new UOW();
            var newUser = new UserDTO()
            {
                Email = "asd3@a.a",
                Password = "123456",
                Name = "user2",
                Role = "manager"
            };
            ApplicationUser user = await uow.UserManager.FindByEmailAsync(newUser.Email);
            if (user == null)
            {
                user = new ApplicationUser() { Email = newUser.Email, UserName = newUser.Name };
                var result = await uow.UserManager.CreateAsync(user, newUser.Password);
                if (result.Errors.Count() == 0)
                {
                    await uow.UserManager.AddToRoleAsync(user.Id, newUser.Role);
                }
                uow.Save();
            }
            user.UserName = "newName";
            await uow.UserManager.UpdateAsync(user);
            uow.Save();
            foreach (var item in uow.UserManager.Users)
            {
                Console.WriteLine($"{item.Email}, {item.PasswordHash}");
            }
        }
    }
}
