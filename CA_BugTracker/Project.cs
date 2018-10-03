using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTracker
{
    public class Project
    {

        static int _id;

        public int Id { get; private set; }


        public string ProjectName { get; set; }

        public Project()
        {
            Id = ++_id;

        }
        public static Project CreateProject()
        {
            Console.WriteLine("Введите название проекта");
            string projectName = Console.ReadLine().ToString();
            Project newproject = new Project
            {
                ProjectName = projectName
            };
            return newproject;
        }
        public static void DeleteProject(List<Project> projects, List<Task> tasks)
        {
            ShowProjects(projects);
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
        public static void ShowProjects(List<Project> projects)
        {
            Console.Clear();
            Console.WriteLine("ID\tНазвание");
            foreach (var item in projects)
            {
                Console.WriteLine("{0:d3}\t{1:15}",item.Id,item.ProjectName);
            }
        }

    }
}
