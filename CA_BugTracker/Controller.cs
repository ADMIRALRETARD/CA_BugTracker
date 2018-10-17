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

        Repository repository;
        public Controller()
        {
            repository = new Repository();
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
            user = repository.IsContainUser(user);

            Console.WriteLine("Введите название проекта");
            string project = Console.ReadLine();
            project = repository.IsContainProject(project);

            Task newtask = new Task
            {
                Theme = theme,
                Type = type,
                Priority = priority,
                Description = description,
                User = repository.users.Find(u => u.LastName == user || u.FirstName == user),
                Project = repository.projects.Find(p => p.ProjectName == project)

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
            repository.AddUser(CreateUser());
        }
        public void AddTask()
        {
            repository.AddTask(CreateTask());
        }
        public void AddProject()
        {
            repository.AddProject(CreateProject());
        }

        #endregion

        #region Show
        public void ShowTasks()
        {
            repository.ShowTasks();
        }
        public void ShowTasksInProject()
        {
            repository.ShowTasksInProject();
        }

        public void ShowTaskForUser()
        {
            repository.ShowTaskForUser();
        }

        public void ShowProjects()
        {
            repository.ShowProjects();
        }
        public void ShowUsers()
        {
            repository.ShowUsers();
        }
        #endregion

        #region Delete

        public void DeleteTask()
        {
            ShowTasks();
            Console.WriteLine("Введите ID задачи , которую следует удалить");
            try
            {

                int removeTask = Convert.ToInt32(Console.ReadLine());
                if (repository.IsContainTask(removeTask))
                {
                    repository.DeleteTask(repository.FindTaskId(removeTask));
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка ввода данных .");
                Console.ReadLine();
                return;
            }
        }
        public void DeleteProject()
        {
            ShowProjects();
            Console.WriteLine("Введите ID проекта, который следует удалить");
            try
            {
                int removeproject = Convert.ToInt32(Console.ReadLine());

                if (repository.IsProjectContainInTask(removeproject))
                {
                    return;
                }
                if (repository.IsProjectContainList(removeproject))
                {
                    repository.DeleteProject(repository.FindProjectId(removeproject));

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
                int removeUser = Convert.ToInt32(Console.ReadLine());
                if (repository.IsContainUserInTask(removeUser))
                {
                    return;
                }
                if (repository.IsUserContainList(removeUser))
                {
                    repository.DeleteUser(repository.FindUserId(removeUser));
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

        #region Save

        public void Savefile()
        {

            string pathtasks = Path.Combine(Environment.CurrentDirectory, "tasks.txt"); ;
            string pathprojects = Path.Combine(Environment.CurrentDirectory, "projects.txt");
            string pathusers = Path.Combine(Environment.CurrentDirectory, "users.txt");
            using (StreamWriter sw = new StreamWriter(pathtasks))
            {
                sw.WriteLine("ID \tНазвание\tИсполнитель\tПроект   \tТип\tПриоритет\tОписание");
                foreach (var item in repository.tasks)
                {
                    sw.WriteLine("{0:d3}\t{1:16}\t{2,-16}{3,-10}{4,10}\t{5,-10}\t{6 ,-5}", item.Id, item.Theme, item.User.LastName
                             , item.Project.ProjectName, item.Type, item.Priority, item.Description);
                }
            }
            using (StreamWriter sw = new StreamWriter(pathprojects))
            {
                sw.WriteLine("ID\tНазвание");
                foreach (var item in repository.projects)
                {
                    sw.WriteLine("{0:d3}\t{1:15}", item.Id, item.ProjectName);
                }
            }
            using (StreamWriter sw = new StreamWriter(pathusers))
            {
                sw.WriteLine("ID\tИмя\tФамилия");
                foreach (var item in repository.users)
                {
                    sw.WriteLine("{0:d3}\t{1:16}\t{2,6}", item.Id, item.FirstName, item.LastName);
                }
            }
            Console.WriteLine("Файлы записаны в каталог программы\\Debug");
        }
        #endregion

    }
}
