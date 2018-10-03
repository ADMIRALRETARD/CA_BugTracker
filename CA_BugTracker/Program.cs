using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("Для навигации введите на клавиатуре соответствующую цифру.\nНажмите любую клавишу для продолжения");
            Console.ReadLine();

            Menu();

            int command;
            while (true)
            {
                switch (command)
                {

                    case 1:
                        Task.ShowTasks(tasks);
                        command = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 2:
                        Options();
                        Menu();
                        break;
                    case 3:
                        ShowMore();
                        break;
                    case 4:

                        break;
                    case 5:

                        Environment.Exit(0);
                        break;
                }
            }






            void Menu()
            {
                Console.Clear();

                Console.WriteLine("1.Показать задачи\n2.Опции\n3.Показать дополнительную информацию\n4.Сохранить на жесткий диск\n5.Выход");
                try
                {
                    command = Convert.ToInt32(Console.ReadLine());
                }

                catch (FormatException)
                {
                    Console.WriteLine("Введите значение");
                    command = Convert.ToInt32(Console.ReadLine());
                }


            }
            void Options()
            {
                int optioncommand;
                Console.Clear();
                Console.WriteLine("1.Добавить задачу\n2.Добавить проект\n3.Добавить пользователя\n4.Удалить задачу" +
                    "\n5.Удалить проект\n6.Удалить пользователя");
                optioncommand = Convert.ToInt32(Console.ReadLine());
                switch (optioncommand)
                {
                    case 1:
                        tasks.Add(Task.CreateTask(users, projects));
                        Console.WriteLine("Задача добавлена");
                        break;
                    case 2:
                        projects.Add(Project.CreateProject());
                        Console.WriteLine("Проект добавлен");
                        break;
                    case 3:
                        users.Add(User.CreateUser());
                        Console.WriteLine("Пользователь добавлен");
                        break;
                    case 4:
                        Task.DeleteTask(tasks);
                        break;
                    case 5:
                        Project.DeleteProject(projects);
                        break;
                    case 6:
                        User.DeleteUser(users);
                        break;
                }
            }
            void ShowMore()
            {
                Console.Clear();
                Console.WriteLine("1.Показать все проекты\n2.Показать всех пользователй" +
                                "\n3.Показать список задач в проекте" +
                                "\n4.Показать список задач,назначенных на конкретного исполнителя");
                int showcommand;
                showcommand = Convert.ToInt32(Console.ReadLine());
                switch (showcommand)
                {
                    case 1:
                        Project.ShowProjects(projects);
                        showcommand = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 2:
                        User.ShowUsers(users);
                        showcommand = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 3:
                        Task.ShowTasksInProject(tasks, projects);
                        showcommand = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 4:
                        Task.ShowTaskForUser(tasks, users);
                        showcommand = Convert.ToInt32(Console.ReadLine());
                        break;
                   

                }

            }





        }


    }
}
