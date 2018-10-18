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

            if (!repository.IsContainUser(user))
            {
                Console.WriteLine("Нельзя добавить несуществующего пользователя");
                Console.ReadLine();
                ShowUsers();
                Console.WriteLine("Введите имя пользователя из списка");

                user = Console.ReadLine();
            }
            Console.WriteLine("Введите название проекта");
            string project = Console.ReadLine();
            if (!repository.IsContainProject(project))
            {
                Console.WriteLine("Нельзя добавить несуществующий проект");
                Console.ReadLine();

                ShowProjects();
                Console.WriteLine("Введите название проекта из списка");

                project = Console.ReadLine();
            }
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
            Console.WriteLine("Пользователь добавлен");
            Console.ReadLine();
        }
        public void AddTask()
        {
            repository.AddTask(CreateTask());
            Console.WriteLine("Задача добавлена");
            Console.ReadLine();
        }
        public void AddProject()
        {
            repository.AddProject(CreateProject());
            Console.WriteLine("Проект добавлен");
            Console.ReadLine();
        }

        #endregion

        #region Show
        public void ShowTasks()
        {
            Console.Clear();
            Console.WriteLine("ID \tНазвание\tИсполнитель\tПроект   \tТип\tПриоритет\tОписание");
            foreach (var item in repository.tasks)
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
            var selected = from t in repository.tasks
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
            var select = from t in repository.tasks
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
            foreach (var item in repository.projects)
            {
                Console.WriteLine("{0:d3}\t{1:15}", item.Id, item.ProjectName);
            }
        }
        public void ShowUsers()
        {
            Console.Clear();
            Console.WriteLine("ID\tИмя\tФамилия");
            foreach (var item in repository.users)
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
            try
            {

                int removeTask = Convert.ToInt32(Console.ReadLine());
                if (repository.IsContainTask(removeTask))
                {
                    repository.DeleteTask(repository.FindTaskId(removeTask));
                    Console.WriteLine("Задача удалена");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Такая задача не существует");
                    Console.ReadLine();
                    return;
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
                    Console.WriteLine("Сначала удалите задачи , связанные с этим проектом");
                    Console.ReadLine();
                    return;
                }
                if (repository.IsProjectContainList(removeproject))
                {
                    repository.DeleteProject(repository.FindProjectId(removeproject));
                    Console.WriteLine("Проект удален");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Такого проекта не существует");
                    Console.ReadLine();
                    return;
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
                    Console.WriteLine("Сначала удалите задачи , связанные с этим пользователем");
                    Console.ReadLine();
                    return;
                }
                if (repository.IsUserContainList(removeUser))
                {
                    repository.DeleteUser(repository.FindUserId(removeUser));
                    Console.WriteLine("Пользователь удален");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Такого пользователя не существует");
                    Console.ReadLine();
                    return;
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
