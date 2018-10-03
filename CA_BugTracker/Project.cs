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
        public static void DeleteProject(List<Project> projects)
        {
            ShowProjects(projects);
            Console.WriteLine("Введите ID проекта, который следует удалить");
            int removeproject = Convert.ToInt32(Console.ReadLine());
            projects.Remove(projects.Find(r => r.Id == removeproject));
        }
        public static void ShowProjects(List<Project> projects)
        {
            Console.Clear();
            foreach (var item in projects)
            {
                Console.WriteLine(item.Id + " :" + item.ProjectName);
            }
        }

    }
}
