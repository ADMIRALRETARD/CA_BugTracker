using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTracker
{
    public class Repository
    {
        public List<Task> tasks;
        public List<User> users;
        public List<Project> projects;

        public Repository()
        {
            projects = new List<Project>
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

            users = new List<User>
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
            tasks = new List<Task>
            {
                new Task
                {
                    Theme ="ЭтоНазвание",
                    Type="Тип1",
                    Priority="Высокий",
                    Description="Описание1",
                    User=users[0],
                    Project=projects[0],
                },
                new Task
                {
                    Theme="ВтороеНазвание",
                    Type="Тип2",
                    Priority="Средний",
                    Description="Описание2",
                    User=users[1],
                    Project=projects[1],

                },
                new Task
                {
                    Theme="ЕщеНазвание",
                    Type="Тип3",
                    Priority="Низкий",
                    Description="Описание",
                    User=users[2],
                    Project=projects[2],
                }
            };
        }

        #region Add

        public void AddTask(Task task)
        {
            tasks.Add(task);
            Console.WriteLine("Задача добавлена");
            Console.ReadLine();
        }
        public void AddProject(Project project)
        {
            projects.Add(project);
            Console.WriteLine("Проект добавлен");
            Console.ReadLine();
        }
        public void AddUser(User user)
        {
            users.Add(user);
            Console.WriteLine("Пользователь добавлен");
            Console.ReadLine();
        }
        #endregion
        #region Delete


        public void DeleteTask()
        {
            ShowTasks();
            Console.WriteLine("Введите ID задачи , которую следует удалить");
            int removeTask = Convert.ToInt32(Console.ReadLine());
            tasks.Remove(FindTaskId(removeTask));

        }


        public void DeleteProject()
        {
            ShowProjects();
            Console.WriteLine("Введите ID проекта, который следует удалить");
            try
            {
                int removeproject = Convert.ToInt32(Console.ReadLine());

                if (IsProjectContainInTask(removeproject))
                {
                    return;
                }
                if (IsProjectContainList(removeproject))
                {
                   
                    projects.Remove(FindProjectId(removeproject));
                    Console.WriteLine("Проект удален");
                    Console.ReadLine();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка ввода данных .");
                Console.ReadLine();
                return;
            }

        }

        public void DeleteUser()
        {
            ShowUsers();
            Console.WriteLine("Введите ID пользователя, которого следует удалить");
            try
            {
                string removeUser = Console.ReadLine();
                if (IsContainTask(removeUser))
                {
                    return;
                }
                if (IsUserContainList(removeUser))
                { 

                users.Remove(FindUserId(removeUser));
                Console.WriteLine("Пользователь удален");
                Console.ReadLine();

                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка ввода данных .");
                Console.ReadLine();
                return;
            }
        }

        #endregion
        #region Show
        public void ShowTasks()
        {
            Console.Clear();
            Console.WriteLine("ID \tНазвание\tИсполнитель\tПроект   \tТип\tПриоритет\tОписание");
            foreach (var item in tasks)
            {//Немного запутался в форматировании
                Console.WriteLine("{0:d3}\t{1:16}\t{2,-16}{3,-10}{4,10}\t{5,-10}\t{6 ,-5}", item.Id, item.Theme, item.User.LastName
                                     , item.Project.ProjectName, item.Type, item.Priority, item.Description);
            }
        }

        public void ShowUsers()
        {
            Console.Clear();
            Console.WriteLine("ID\tИмя\tФамилия");
            foreach (var item in users)
            {
                Console.WriteLine("{0:d3}\t{1:16}\t{2,6}", item.Id, item.FirstName, item.LastName);
            }
        }

        public void ShowProjects()
        {
            Console.Clear();
            Console.WriteLine("ID\tНазвание");
            foreach (var item in projects)
            {
                Console.WriteLine("{0:d3}\t{1:15}", item.Id, item.ProjectName);
            }
        }
        public void ShowTasksInProject()
        {
            Console.Clear();
            ShowProjects();
            Console.WriteLine("Введите название проекта,для которого необходимо отобразить задачи");
            string projectName = Console.ReadLine();
            var selected = from t in tasks
                           where t.Project.ProjectName == projectName
                           select t;
            Console.WriteLine("Задачи в проекте : {0}", projectName);
            foreach (var item in selected)
            {
                Console.WriteLine(item.Theme);
            }
        }

        public void ShowTaskForUser()
        {
            Console.Clear();
            ShowUsers();
            Console.WriteLine("Введите Фамилию исполнителя");
            string userName = Console.ReadLine();
            var select = from t in tasks
                         where t.User.LastName == userName
                         select t;
            Console.WriteLine("Задачи назначенные исполнителю :{0}", userName);
            foreach (var item in select)
            {
                Console.WriteLine("Название : " + item.Theme);
            }

        }
        #endregion
        //Проверки на наличие в списках
        #region Validation
        //Проверка на наличие Пользователя в списке Пользователя(для контроллера)
        public string IsContainUser(string user)
        {
            while (!users.Contains(users.Find(u => u.FirstName == user || u.LastName == user)))
            {
                Console.WriteLine("Нельзя добавить несуществующего пользователя");
                Console.ReadLine();
                ShowUsers();
                Console.WriteLine("Введите имя пользователя из списка");

                user = Console.ReadLine();
            }
            return user;

        }
        //Проверка на наличие Проекта в списке Проекты(для контроллера)
        public string IsContainProject(string project)
        {
            while (!projects.Contains(projects.Find(p => p.ProjectName == project)))
            {
                Console.WriteLine("Нельзя добавить несуществующий проект");
                Console.ReadLine();
                ShowProjects();
                Console.WriteLine("Введите название проекта из списка");

                project = Console.ReadLine();
            }
            return project;
        }
        //Проверка на наличие пользователя в списке Задачи
        private bool IsContainTask(string removeUser)
        {
            while (tasks.Contains(tasks.Find(p => p.User.Id == Convert.ToInt32(removeUser))))
            {
                Console.WriteLine("Сначала удалите задачи , связанные с этим проектом");
                Console.ReadLine();
                return true;

            }
            return false;
        }
        //Проверка наличия пользователя в списке Пользователи
        private bool IsUserContainList(string removeUser)
        {
            if (!users.Contains(users.Find(u => u.Id == Convert.ToInt32(removeUser))))
            {
                Console.WriteLine("Такого пользователя не существует");
                Console.ReadLine();
                return false;
            }
            return true;
        }
        //Проверка на наличие проекта в списке Задачи
        private bool IsProjectContainInTask(int removeProject)
        {
            while (tasks.Contains(tasks.Find(p => p.Project.Id == removeProject)))
            {
                Console.WriteLine("Сначала удалите задачи , связанные с этим проектом");
                Console.ReadLine();
                return true;
            }
            return false;
        }
        //Проверка на наличие проекта в списке Проекты
        private bool IsProjectContainList(int removeProject)
        {
            if (!projects.Contains(projects.Find(u => u.Id == Convert.ToInt32(removeProject))))
            {
                Console.WriteLine("Такого проекта не существует");
                Console.ReadLine();
                return false;
            }
            return true;
        }
        #endregion
        #region FindObjects
        User FindUserId(string removeUser)
        {
            return users.Find(u => u.Id == Convert.ToInt32(removeUser));
        }
        Task FindTaskId(int removeTask)
        {
            return tasks.Find(f => f.Id == removeTask);
        }
        Project FindProjectId(int removeProject)
        {
            return projects.Find(r => r.Id == removeProject);
        }
        #endregion
    }
}
