using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CA_BugTracker
{
    class Program
    {

        static void Main(string[] args)
        {
            #region Fill
            //заполнение начальными значениями
            List<Project> projects = new List<Project>
            {
                new Project
                {
                  ProjectName="Один"
                },
                new Project{
                  ProjectName="ПроектДва"
                },
                new Project{
                  ProjectName="НомерТри"
                },
            };


            List<User> users = new List<User>
            {
                new User
                {
                    FirstName="Евгений",
                    LastName="Замятин"
                },
                new User
                {
                    FirstName="Эмили",
                    LastName="Бронте"
                },
                new User
                {
                    FirstName="Линус",
                    LastName="Торвальдс"
                }
            };
            List<Task> tasks = new List<Task>
            {
                new Task
                {
                    Theme ="ЭтоНазвание",
                    Type="Тип1",
                    Priority="Высокий",
                    Description="Описание1",
                    User=users[0],
                    Project=projects[0],
                    ProjectName =projects[0].ProjectName
                },
                new Task
                {
                    Theme="ВтороеНазвание",
                    Type="Тип2",
                    Priority="Средний",
                    Description="Описание2",
                    User=users[1],
                    Project=projects[1],
                    ProjectName =projects[2].ProjectName
                   // UserName=users[1].LastName
                },
                new Task
                {
                    Theme="ЕщеНазвание",
                    Type="Тип3",
                    Priority="Низкий",
                    Description="Описание",
                    User=users[2],
                    Project=projects[2],
                    ProjectName =projects[2].ProjectName
                }
            };
            #endregion
            Console.WriteLine("Для навигации введите на клавиатуре соответствующую цифру.\nНажмите любую клавишу для продолжения...");
            Console.ReadLine();

            string command;
            Menu();

            while (true)
            {
                switch (command)
                {

                    case "1":
                        Task.ShowTasks(tasks);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        command =Console.ReadLine();
                        break;
                    case "2":
                        Options();
                        Menu();
                        break;
                    case "3":
                        ShowMore();
                        break;
                    case "4":
                        Savefile();
                        command = Console.ReadLine();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        
                        Menu();
                        break;
                }
            }






            void Menu()
            {
                Console.Clear();

                Console.WriteLine("1.Показать задачи\n2.Опции\n3.Показать дополнительную информацию\n4.Сохранить на жесткий диск\n5.Выход");
                
                command = Console.ReadLine();
                if (command == "")
                {
                    command = Console.ReadLine();
                }
            }
            void Options()
            {
                string optioncommand;
                Console.Clear();
                Console.WriteLine("1.Добавить задачу\n2.Добавить проект\n3.Добавить пользователя\n4.Удалить задачу" +
                    "\n5.Удалить проект\n6.Удалить пользователя" +
                    "\n7.Главное меню");
                optioncommand = Console.ReadLine();
                if (optioncommand == "")
                {
                    optioncommand = Console.ReadLine();
                }
                switch (optioncommand)
                {
                    case "1":
                        tasks.Add(Task.CreateTask(users, projects));
                        Console.WriteLine("Задача добавлена");
                        Console.ReadLine();
                        break;
                    case "2":
                        projects.Add(Project.CreateProject());
                        Console.WriteLine("Проект добавлен");
                        Console.ReadLine();
                        break;
                    case "3":
                        users.Add(User.CreateUser());
                        Console.WriteLine("Пользователь добавлен");
                        Console.ReadLine();
                        break;
                    case "4":
                        Task.DeleteTask(tasks);
                        break;
                    case "5":
                        Project.DeleteProject(projects,tasks);
                        break;
                    case "6":
                        User.DeleteUser(users,tasks);
                        break;
                    case "7":
                        Menu();
                        break;
                    default:
                        Console.WriteLine("Вы не ввели число");
                        Options();
                        optioncommand = Console.ReadLine();
                        break;
                }
            }
            void ShowMore()
            {
                Console.Clear();
                Console.WriteLine("1.Показать все проекты\n2.Показать всех пользователй" +
                                "\n3.Показать список задач в проекте" +
                                "\n4.Показать список задач,назначенных на конкретного исполнителя" +
                                "\n5.Главное меню");
                string showcommand;
                showcommand = Console.ReadLine();
                switch (showcommand)
                {
                    case "1":
                        Project.ShowProjects(projects);
                        showcommand = Console.ReadLine();
                        break;
                    case "2":
                        User.ShowUsers(users);
                        showcommand = Console.ReadLine();
                        break;
                    case "3":
                        Task.ShowTasksInProject(tasks, projects);
                        showcommand = Console.ReadLine();
                        break;
                    case "4":
                        Task.ShowTaskForUser(tasks, users);
                        showcommand = Console.ReadLine();
                        break;
                    case "5":
                        Menu();
                        break;
                    default:
                        Menu();
                        break;
                   

                }

            }
            void Savefile()
            {
               
                string pathtasks = Environment.CurrentDirectory.ToString() +"\\tasks.txt";
                string pathprojects = Environment.CurrentDirectory.ToString() + "\\projects.txt";
                string pathusers = Environment.CurrentDirectory.ToString() + "\\users.txt";
                using (StreamWriter sw=new StreamWriter(pathtasks))
                {
                    sw.WriteLine("ID \tНазвание\tИсполнитель\tПроект   \tТип\tПриоритет\tОписание");
                    foreach (var item in tasks)
                    {
                        sw.WriteLine("{0:d3}\t{1:16}\t{2,-16}{3,-10}{4,10}\t{5,-10}\t{6 ,-5}", item.Id, item.Theme, item.User.LastName
                                 , item.Project.ProjectName, item.Type, item.Priority, item.Description);
                    }
                }
                using (StreamWriter sw = new StreamWriter(pathprojects))
                {
                    sw.WriteLine("ID\tНазвание");
                    foreach (var item in projects)
                    {
                        sw.WriteLine("{0:d3}\t{1:15}", item.Id, item.ProjectName);
                    }
                }
                using (StreamWriter sw = new StreamWriter(pathusers))
                {   sw.WriteLine("ID\tИмя\tФамилия");
                    foreach (var item in users)
                    {
                        sw.WriteLine("{0:d3}\t{1:16}\t{2,6}",item.Id ,item.FirstName, item.LastName);
                    }
                }
                Console.WriteLine("Файлы записаны в каталог программы");
            }





        }


    }
}
