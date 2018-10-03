using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTracker
{
    public class User
    {
        static int _id;
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User()
        {
            Id = ++_id;
        }

        public static User CreateUser()
        {
            Console.WriteLine("Введите имя пользователя");
            string firstName = Console.ReadLine().ToString();
            Console.WriteLine("Введите фамилию пользователя");
            string lastName = Console.ReadLine().ToString();
            User newuser = new User()
            {

                FirstName = firstName,
                LastName = lastName

            };

            return newuser;
        }
        public static void ShowUsers(List<User> users)
        {
            Console.Clear();
            foreach (var item in users)
            {
                Console.WriteLine(item.Id + " :" + item.FirstName + "  :" + item.LastName);
            }

        }
        public static void DeleteUser(List<User> users)
        {
            ShowUsers(users);
            Console.WriteLine("Введите Имя,Фамилию или ID пользователя, которого следует удалить");
            string removeUser = Console.ReadLine();
            users.Remove(users.Find(u => u.LastName == removeUser || u.FirstName == removeUser || u.Id == Convert.ToInt32(removeUser)));
        }
    }
}
