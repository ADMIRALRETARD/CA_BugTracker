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
            Console.WriteLine("ID\tИмя\tФамилия");
            foreach (var item in users)
            {
                Console.WriteLine("{0:d3}\t{1:16}\t{2,6}",item.Id ,item.FirstName, item.LastName);
            }

        }
        public static void DeleteUser(List<User> users, List<Task> tasks)
        {
            ShowUsers(users);
            Console.WriteLine("Введите ID пользователя, которого следует удалить");
            try
            {

                string removeUser = Console.ReadLine();
                while (tasks.Contains(tasks.Find(p => p.User.Id == Convert.ToInt32(removeUser))))
                {
                    Console.WriteLine("Сначала удалите задачи , связанные с этим проектом");
                    Console.ReadLine();
                    return;
                }
                if (!users.Contains(users.Find(u => u.Id == Convert.ToInt32(removeUser))))
                {
                    Console.WriteLine("Такого пользователя не существует");
                    Console.ReadLine();
                    return;
                }
                users.Remove(users.Find(u => u.Id == Convert.ToInt32(removeUser)));
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка ввода данных .");
                Console.ReadLine();
                return;
            }
        }
    }
}
