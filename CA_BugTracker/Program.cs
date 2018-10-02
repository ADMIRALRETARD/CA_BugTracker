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
                  ProjectName="Проект1"
                },
                new Project{
                  ProjectName="Проект2"
                },
                new Project{
                  ProjectName="Проект3"
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
                    Theme ="Название1",
                    Type="Тип1",
                    Priority="Высокий",
                    Description="Описание1",
                    User=users[0],
                    Project=projects[0]
                },
                new Task
                {
                    Theme="Название2",
                    Type="Тип2",
                    Priority="Средний",
                    Description="Описание2",
                    User=users[1],
                    Project=projects[1]
                },
                new Task
                {
                    Theme="Название3",
                    Type="Тип3",
                    Priority="Низкий",
                    Description="Описание",
                    User=users[2],
                    Project=projects[2]
                }
            };
            #endregion
            Console.WriteLine("Для навигации введите на клавиатуре соответствующую цифру.\nНажмите любую клавишу для продолжения");
            Console.ReadLine();
            int command;
            
                Menu();
                while (command != 4)
                {

                    switch (command)
                    {

                        case 1:
                            ShowTasks();
                            command = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 2:
                            Options();
                            Menu();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                    }
                }

            
           


            // Console.ReadLine();
            void Menu()
            {
                Console.Clear();

                Console.WriteLine("1.Показать задачи\n2.Опции\n3.Запросы\n4.Выход");
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
                Console.WriteLine("1.Добавить задачу\n2.Добавить проект\n3.Добавить пользователя\n4.Удалить задачу" +
                    "\n5.Удалить проект\n6.Удалить пользователя");
                command = Convert.ToInt32(Console.ReadLine());
                switch (command)
                {
                    case 1:
                        AddNewTask();
                        break;
                    case 2:
                        AddNewProject();
                        break;
                    case 3:
                        AddNewUser();
                        break;
                    case 4:
                        DeleteTask();
                        break;
                    case 5:
                        DeleteProject();
                        break;
                    case 6:
                        DeleteUser();
                        break;
                }
            }
            #region Delete
            void DeleteProject()
            {
                ShowProjects();
                Console.WriteLine("Введите название проекта или ID, который следует удалить");
                string removeproject = Console.ReadLine();
                projects.Remove(projects.Find(r => r.ProjectName == removeproject || r.Id == Convert.ToInt32(removeproject)));
            }
            void DeleteUser()
            {
                ShowUsers();
                Console.WriteLine("Введите Имя,Фамилию или ID пользователя, которого следует удалить");
                string removeUser = Console.ReadLine();
                users.Remove(users.Find(u => u.LastName == removeUser || u.FirstName == removeUser || u.Id == Convert.ToInt32(removeUser)));
            }
            void DeleteTask()
            {
                ShowTasks();
                Console.WriteLine("Введите Название или ID задачи , которую следует удалить");
                string removeTask = Console.ReadLine();
                tasks.Remove(tasks.Find(f => f.Theme == removeTask || f.Id == Convert.ToInt32(removeTask)));

            }

            #endregion
            #region Show
            void ShowTasks()
            {
                Console.Clear();
                foreach (var item in tasks)
                {
                    Console.WriteLine(item.Id + " Nazvanie:" + item.Theme + "  UserName:"
                        + item.User.LastName + " ProjectName:" + item.Project.ProjectName
                        + " Type:" + item.Type + " Description:" + item.Description);
                }
            }
            void ShowUsers()
            {
                Console.Clear();
                foreach (var item in users)
                {
                    Console.WriteLine(item.Id + " :" + item.FirstName + "  :" + item.LastName);
                }
            }
            void ShowProjects()
            {
                Console.Clear();
                foreach (var item in projects)
                {
                    Console.WriteLine(item.Id + " :" + item.ProjectName);
                }
            }
            #endregion
            #region Add

            void AddNewTask()
            {
                Console.WriteLine("Введите название задачи :");
                string theme = Console.ReadLine();
                Console.WriteLine("Введите тип задачи");
                string type = Console.ReadLine();
                Console.WriteLine("Введите приоритет задачи");
                string priority = Console.ReadLine();
                Console.WriteLine("Введите описание задачи");
                string description = Console.ReadLine();

                Console.WriteLine("Введите имя или фамилию исполнителя");
                string user = Console.ReadLine();

                Console.WriteLine("Введите название проекта");
                string project = Console.ReadLine();


                Task newTask = new Task
                {

                    Theme = theme,
                    Type = type,
                    Priority = priority,
                    Description = description,
                    User = users.Find(u => u.LastName == user || u.FirstName == user),
                    Project = projects.Find(p => p.ProjectName == project)
                };
                tasks.Add(newTask);
                Console.WriteLine("Задача добавлена");

            }
            void AddNewProject()
            {
                Console.WriteLine("Введите название проекта");
                string projectName = Console.ReadLine().ToString();
                Project newproject = new Project
                {
                    ProjectName = projectName
                };
                projects.Add(newproject);
                Console.WriteLine("Проект добавлен");
            }
            void AddNewUser()
            {
                Console.WriteLine("Введите имя пользователя");
                string firstName = Console.ReadLine().ToString();
                Console.WriteLine("Введите фамилию пользователя");
                string lastName = Console.ReadLine().ToString();
                User newUser = new User
                {
                    FirstName = firstName,
                    LastName = lastName
                };
                users.Add(newUser);
                Console.WriteLine("Пользователь добавлен");
            }
        }
        #endregion


    }
}
