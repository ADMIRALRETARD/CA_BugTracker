using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTracker
{
    public class Controller
    {
        List<Task> tasks;
        List<User> users;
        List<Project> projects;
        
        public Controller()
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
        
        #region Create

        Task CreateTask()
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
            while (!users.Contains(users.Find(u => u.FirstName == user || u.LastName == user)))
            {
                Console.WriteLine("Нельзя добавить несуществующего пользователя");
                ShowUsers();

                user = Console.ReadLine();
            }
            Console.WriteLine("Введите название проекта");
            string project = Console.ReadLine();
            while (!projects.Contains(projects.Find(p => p.ProjectName == project)))
            {
                Console.WriteLine("Нельзя добавить несуществующий проект");
                ShowProjects();
                project = Console.ReadLine();
            }
            Task newtask = new Task
            {
                Theme = theme,
                Type = type,
                Priority = priority,
                Description = description,
                User = users.Find(u => u.LastName == user || u.FirstName == user),
                Project = projects.Find(p => p.ProjectName == project)

            };

            return newtask;
        }
        User CreateUser()
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
        Project CreateProject()
        {
            Console.WriteLine("Введите название проекта");
            string projectName = Console.ReadLine().ToString();
            Project newproject = new Project
            {
                ProjectName = projectName
            };
            return newproject;
        }
        #endregion
        
        #region Add

        public void AddUser()
        {
            users.Add(CreateUser());
            Console.WriteLine("Пользователь добавлен");
            Console.ReadLine();
        }
        public void AddTask()
        {
            tasks.Add(CreateTask());
            Console.WriteLine("Задача добавлена");
            Console.ReadLine();
        }
        public void AddProject()
        {
            projects.Add(CreateProject());
            Console.WriteLine("Проект добавлен");
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

        public  void ShowTaskForUser()
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

        public void ShowProjects()
        {
            Console.Clear();
            Console.WriteLine("ID\tНазвание");
            foreach (var item in projects)
            {
                Console.WriteLine("{0:d3}\t{1:15}", item.Id, item.ProjectName);
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
        #endregion

        #region Delete

        public void DeleteTask()
        {
            ShowTasks();
            Console.WriteLine("Введите ID задачи , которую следует удалить");
            int removeTask = Convert.ToInt32(Console.ReadLine());
            tasks.Remove(tasks.Find(f => f.Id == removeTask));

        }


        public void DeleteProject()
        {
            ShowProjects();
            Console.WriteLine("Введите ID проекта, который следует удалить");
            try
            {

                int removeproject = Convert.ToInt32(Console.ReadLine());

                while (tasks.Contains(tasks.Find(p => p.Project.Id == removeproject)))
                {
                    Console.WriteLine("Сначала удалите задачи , связанные с этим проектом");
                    Console.ReadLine();
                    return;

                }
                if (!projects.Contains(projects.Find(u => u.Id == Convert.ToInt32(removeproject))))
                {
                    Console.WriteLine("Такого проекта не существует");
                    Console.ReadLine();
                    return;
                }
                projects.Remove(projects.Find(r => r.Id == removeproject));
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка ввода данных .");
                Console.ReadLine();
                return;
            }

        }

        public  void DeleteUser()
        {
            ShowUsers();
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


        #endregion

        #region Save

       public void Savefile()
        {

            string pathtasks = Path.Combine(Environment.CurrentDirectory, "tasks.txt"); ;
            string pathprojects = Path.Combine(Environment.CurrentDirectory, "projects.txt");
            string pathusers = Path.Combine(Environment.CurrentDirectory, "users.txt");
            using (StreamWriter sw = new StreamWriter(pathtasks))
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
            {
                sw.WriteLine("ID\tИмя\tФамилия");
                foreach (var item in users)
                {
                    sw.WriteLine("{0:d3}\t{1:16}\t{2,6}", item.Id, item.FirstName, item.LastName);
                }
            }
            Console.WriteLine("Файлы записаны в каталог программы\\Debug");
        }
        #endregion

    }
}
