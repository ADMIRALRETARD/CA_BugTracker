using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTracker
{
    public class Task
    {

        static int _id;
        public int Id { get; private set; }
        public string Theme { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }


        public User User { get; set; }
        public Project Project { get; set; }

        public Task()
        {
            Id = ++_id;


        }
        public static Task CreateTask(List<User> users, List<Project> projects)
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
                User.ShowUsers(users);
                user = Console.ReadLine();
            }
            Console.WriteLine("Введите название проекта");
            string project = Console.ReadLine();
            while (!projects.Contains(projects.Find(p => p.ProjectName == project)))
            {
                Console.WriteLine("Нельзя добавить несуществующего пользователя");
                Project.ShowProjects(projects);
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
        public static void DeleteTask(List<Task> tasks)
        {
            ShowTasks(tasks);
            Console.WriteLine("Введите ID задачи , которую следует удалить");
            int removeTask = Convert.ToInt32(Console.ReadLine());
            tasks.Remove(tasks.Find(f => f.Id == removeTask));

        }
        public static void ShowTasks(List<Task> tasks)
        {
            Console.Clear();
            Console.WriteLine("ID \tНазвание\tИсполнитель\tПроект   \tТип\tПриоритет\tОписание");
            foreach (var item in tasks)
            {//Немного запутался в форматировании
            Console.WriteLine("{0:d3}\t{1:16}\t{2,-16}{3,-10}{4,10}\t{5,-10}\t{6 ,-5}",item.Id,item.Theme,item.User.LastName
                                 ,item.Project.ProjectName,item.Type,item.Priority,item.Description);
            }
           
        }
        public static void ShowTasksInProject(List<Task> tasks, List<Project> projects)
        {
            Console.Clear();
            Project.ShowProjects(projects);
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
        public static void ShowTaskForUser(List<Task> tasks, List<User> users)
        {
            Console.Clear();
            User.ShowUsers(users);
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

    }


}
