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


        public void DeleteTask(Task task)
        {
            tasks.Remove(task);
            Console.WriteLine("Задача удалена");
            Console.ReadLine();
        }


        public void DeleteProject(Project project)
        {
            projects.Remove(project);
            Console.WriteLine("Проект удален");
            Console.ReadLine();
            
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
            Console.WriteLine("Пользователь удален");
            Console.ReadLine();

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

       // Проверка на наличие задачи в списке Задачи
        public bool IsContainTask(int removeTask)
        {
            while (FindTaskId(removeTask)==null)
            {
                Console.WriteLine("Такая задача не существует");
                Console.ReadLine();
                return false;
            }
            return true;
        }
        //Проверка на наличие пользователя в списке Задачи
        public bool IsContainUserInTask(int removeUser)
        {
            while (tasks.Contains(tasks.Find(u => u.User.Id == Convert.ToInt32(removeUser))))
            {
                Console.WriteLine("Сначала удалите задачи , связанные с этим пользователем");
                Console.ReadLine();
                return true;

            }
            return false;
        }
        //Проверка наличия пользователя в списке Пользователи
        public bool IsUserContainList(int removeUser)
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
        public bool IsProjectContainInTask(int removeProject)
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
        public bool IsProjectContainList(int removeProject)
        {
            if (!projects.Contains(FindProjectId(removeProject)))
            {
                Console.WriteLine("Такого проекта не существует");
                Console.ReadLine();
                return false;
            }
            return true;
        }
        #endregion
        #region FindObjects
        public User FindUserId(int removeUser)
        {
            return users.Find(u => u.Id == Convert.ToInt32(removeUser));
        }
        public Task FindTaskId(int removeTask)
        {
            return tasks.Find(f => f.Id == removeTask);
        }
        public Project FindProjectId(int removeProject)
        {
            return projects.Find(r => r.Id == removeProject);
        }
        #endregion
    }
}
